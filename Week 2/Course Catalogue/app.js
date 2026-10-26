const state = {
  apiUrl: localStorage.getItem('apiUrl') || 'http://localhost:5000/api',
  isOnline: false,
  courses: [],
  learners: [],
  payments: []
};

// DOM References
const elements = {
  apiDot: document.getElementById('apiDot'),
  apiStatusText: document.getElementById('apiStatusText'),
  apiUrlInput: document.getElementById('apiUrl'),
  reconnectBtn: document.getElementById('reconnectBtn'),
  cliLog: document.getElementById('cliLog'),
  pageTitle: document.getElementById('pageTitle'),
  
  // Dashboard
  statTotalCourses: document.getElementById('statTotalCourses'),
  statTotalLearners: document.getElementById('statTotalLearners'),
  statTotalPayments: document.getElementById('statTotalPayments'),
  recentEventsList: document.getElementById('recentEventsList'),

  // Tables
  courseTableBody: document.getElementById('courseTableBody'),
  learnerTableBody: document.getElementById('learnerTableBody'),
  
  // Modals
  courseModal: document.getElementById('courseModal'),
  learnerModal: document.getElementById('learnerModal'),
  openCourseModalBtn: document.getElementById('openCourseModalBtn'),
  openLearnerModalBtn: document.getElementById('openLearnerModalBtn'),

  // Select Options
  calcLearner: document.getElementById('calcLearner'),
  calcCourse: document.getElementById('calcCourse'),
  payLearner: document.getElementById('payLearner')
};

// Initialize Application
document.addEventListener('DOMContentLoaded', () => {
  setupNavigation();
  setupEventListeners();
  checkApiHealth();
  
  // Initial Local Storage Load
  loadFromLocalStorage();
});

// Logger Utility
function logCli(message, type = 'success') {
  const time = new Date().toLocaleTimeString();
  const line = document.createElement('div');
  line.className = `cli-line ${type}`;
  line.textContent = `[${time}] ${message}`;
  elements.cliLog.appendChild(line);
  elements.cliLog.scrollTop = elements.cliLog.scrollHeight;
}

function showToast(message, type = 'success') {
  const container = document.getElementById('toastContainer');
  const toast = document.createElement('div');
  toast.className = `toast ${type}`;
  toast.textContent = message;
  container.appendChild(toast);
  setTimeout(() => toast.remove(), 3000);
}

// Check Connection to C# Web API
async function checkApiHealth() {
  state.apiUrl = elements.apiUrlInput.value.replace(/\/$/, '');
  localStorage.setItem('apiUrl', state.apiUrl);

  logCli(`Pinging backend API at ${state.apiUrl}/courses...`, 'info');

  try {
    const controller = new AbortController();
    const timeoutId = setTimeout(() => controller.abort(), 3000);

    const res = await fetch(`${state.apiUrl}/courses`, { signal: controller.signal });
    clearTimeout(timeoutId);

    if (res.ok) {
      state.isOnline = true;
      elements.apiDot.className = 'status-dot online';
      elements.apiStatusText.textContent = 'API Connected';
      logCli('C# Web API Connection Established successfully!', 'success');
      showToast('Connected to C# Backend API');
      fetchDataFromApi();
    } else {
      throw new Error(`HTTP Status ${res.status}`);
    }
  } catch (err) {
    state.isOnline = false;
    elements.apiDot.className = 'status-dot offline';
    elements.apiStatusText.textContent = 'Offline (Fallback Mode)';
    logCli(`API Connection Failed (${err.message}). Using local browser state.`, 'error');
  }
}

// Fetch Initial Data from Server
async function fetchDataFromApi() {
  if (!state.isOnline) return;

  try {
    const [coursesRes, learnersRes] = await Promise.all([
      fetch(`${state.apiUrl}/courses`),
      fetch(`${state.apiUrl}/learners`)
    ]);

    if (coursesRes.ok) {
      state.courses = await coursesRes.ok ? await coursesRes.json() : state.courses;
    }
    if (learnersRes.ok) {
      state.learners = await learnersRes.ok ? await learnersRes.json() : state.learners;
    }

    renderAll();
    logCli('Refreshed state from C# API backend.', 'success');
  } catch (err) {
    logCli(`Error syncing with API: ${err.message}`, 'error');
  }
}

// UI Navigation Tab Switching
function setupNavigation() {
  document.querySelectorAll('.nav-item').forEach(btn => {
    btn.addEventListener('click', () => {
      document.querySelectorAll('.nav-item').forEach(b => b.classList.remove('active'));
      document.querySelectorAll('.tab-pane').forEach(p => p.classList.remove('active'));

      btn.classList.add('active');
      const tabId = btn.getAttribute('data-tab');
      document.getElementById(`tab-${tabId}`).classList.add('active');
      elements.pageTitle.textContent = btn.innerText.trim();
    });
  });
}

// Event Listeners Setup
function setupEventListeners() {
  elements.reconnectBtn.addEventListener('click', checkApiHealth);
  
  // Clear Console
  document.getElementById('clearCliBtn').addEventListener('click', () => {
    elements.cliLog.innerHTML = '';
  });

  // Modal Open / Close Controls
  elements.openCourseModalBtn.addEventListener('click', () => elements.courseModal.classList.remove('hidden'));
  elements.openLearnerModalBtn.addEventListener('click', () => elements.learnerModal.classList.remove('hidden'));

  document.querySelectorAll('.close-modal').forEach(span => {
    span.addEventListener('click', () => {
      elements.courseModal.classList.add('hidden');
      elements.learnerModal.classList.add('hidden');
    });
  });

  // Course Form Submit
  document.getElementById('courseForm').addEventListener('submit', async (e) => {
    e.preventDefault();
    const newCourse = {
      code: document.getElementById('courseCode').value,
      title: document.getElementById('courseTitle').value,
      category: document.getElementById('courseCategory').value,
      fee: parseFloat(document.getElementById('courseFee').value),
      duration: document.getElementById('courseDuration').value
    };

    if (state.isOnline) {
      try {
        const res = await fetch(`${state.apiUrl}/courses`, {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify(newCourse)
        });
        if (res.ok) logCli(`API POST Success: Added course ${newCourse.code}`, 'success');
      } catch (err) {
        logCli(`API POST Failed: ${err.message}`, 'error');
      }
    }

    state.courses.push(newCourse);
    saveToLocalStorage();
    renderAll();
    elements.courseModal.classList.add('hidden');
    e.target.reset();
    showToast('Course saved successfully!');
  });

  // Learner Form Submit
  document.getElementById('learnerForm').addEventListener('submit', async (e) => {
    e.preventDefault();
    const newLearner = {
      id: document.getElementById('learnerId').value,
      name: document.getElementById('learnerName').value,
      contact: document.getElementById('learnerContact').value,
      email: document.getElementById('learnerEmail').value
    };

    if (state.isOnline) {
      try {
        const res = await fetch(`${state.apiUrl}/learners`, {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify(newLearner)
        });
        if (res.ok) logCli(`API POST Success: Registered learner ${newLearner.id}`, 'success');
      } catch (err) {
        logCli(`API POST Failed: ${err.message}`, 'error');
      }
    }

    state.learners.push(newLearner);
    saveToLocalStorage();
    renderAll();
    elements.learnerModal.classList.add('hidden');
    e.target.reset();
    showToast('Learner registered successfully!');
  });

  // Fee Calculator Form
  document.getElementById('calcForm').addEventListener('submit', (e) => {
    e.preventDefault();
    const courseCode = elements.calcCourse.value;
    const discount = parseFloat(document.getElementById('calcDiscount').value) || 0;
    
    // Find course safely checking multiple possible property names (code, id, courseCode)
    const course = state.courses.find(c => 
      (c.code && c.code.toString() === courseCode) || 
      (c.id && c.id.toString() === courseCode) || 
      (c.courseCode && c.courseCode.toString() === courseCode)
    );

    // Find fee safely checking lowercase or uppercase
    const baseFee = course ? (course.fee !== undefined ? course.fee : course.Fee !== undefined ? course.Fee : 0) : 0;

    if (course && baseFee > 0) {
      const finalFee = baseFee - (baseFee * (discount / 100));

      document.getElementById('resBaseFee').textContent = `$${parseFloat(baseFee).toFixed(2)}`;
      document.getElementById('resDiscount').textContent = `${discount}%`;
      document.getElementById('resFinalFee').textContent = `$${finalFee.toFixed(2)}`;
      document.getElementById('calcResult').classList.remove('hidden');

      logCli(`Calculated Fee for Course: Base $${baseFee} -> Final $${finalFee.toFixed(2)}`, 'info');
    } else {
      showToast('Please select a valid course with a fee.', 'error');
      logCli(`Fee calculation failed: Course not found or fee is 0 for code "${courseCode}"`, 'error');
    }
  });

  // Payment Form Submit
  document.getElementById('paymentForm').addEventListener('submit', (e) => {
    e.preventDefault();
    const learnerId = elements.payLearner.value;
    const amount = parseFloat(document.getElementById('payAmount').value);
    const method = document.getElementById('payMethod').value;
    const learner = state.learners.find(l => l.id === learnerId);
    const payment = {
      id: 'REC-' + Math.floor(100000 + Math.random() * 900000),
      learnerName: learner ? learner.name : learnerId,
      amount: amount,
      method: method,
      date: new Date().toLocaleDateString()
    };

    state.payments.push(payment);
    saveToLocalStorage();
    renderAll();
    e.target.reset();
    showToast('Payment processed!');
    logCli(`Processed payment ${payment.id} of $${amount} via ${method}`, 'success');
  });
}

// Local Storage Helper Functions
function saveToLocalStorage() {
  localStorage.setItem('local_courses', JSON.stringify(state.courses));
  localStorage.setItem('local_learners', JSON.stringify(state.learners));
  localStorage.setItem('local_payments', JSON.stringify(state.payments));
}

function loadFromLocalStorage() {
  state.courses = JSON.parse(localStorage.getItem('local_courses')) || [];
  state.learners = JSON.parse(localStorage.getItem('local_learners')) || [];
  state.payments = JSON.parse(localStorage.getItem('local_payments')) || [];
  renderAll();
}

// Rendering UI Tables and Options
function renderAll() {
  renderMetrics();
  renderCourses();
  renderLearners();
  renderOptions();
  renderReceipts();
}

function renderMetrics() {
  elements.statTotalCourses.textContent = state.courses.length;
  elements.statTotalLearners.textContent = state.learners.length;
  const totalRev = state.payments.reduce((sum, p) => sum + p.amount, 0);
  elements.statTotalPayments.textContent = `$${totalRev.toFixed(2)}`;
}

function renderCourses() {
  if (state.courses.length === 0) {
    elements.courseTableBody.innerHTML = '<tr><td colspan="6" class="text-center">No courses found.</td></tr>';
    return;
  }

  elements.courseTableBody.innerHTML = state.courses.map(c => {
    // Check both uppercase (C# default) and lowercase property names
    const code = c.code || c.Code || c.courseCode || c.CourseCode || 'N/A';
    const title = c.title || c.Title || c.name || c.Name || 'Untitled';
    const category = c.category || c.Category || 'General';
    const fee = c.fee !== undefined ? c.fee : (c.Fee !== undefined ? c.Fee : 0);
    const duration = c.duration || c.Duration || 'N/A';

    return `
      <tr>
        <td><strong>${code}</strong></td>
        <td>${title}</td>
        <td>${category}</td>
        <td>$${parseFloat(fee).toFixed(2)}</td>
        <td>${duration}</td>
        <td><button class="btn-danger" onclick="deleteCourse('${code}')"><i class="fa-solid fa-trash"></i></button></td>
      </tr>
    `;
  }).join('');
}

function renderLearners() {
  if (state.learners.length === 0) {
    elements.learnerTableBody.innerHTML = '<tr><td colspan="5" class="text-center">No learners registered.</td></tr>';
    return;
  }

  elements.learnerTableBody.innerHTML = state.learners.map(l => {
    // Check both uppercase (C# default) and lowercase property names
    const id = l.id || l.Id || l.learnerId || l.LearnerId || l.learnerID || 'N/A';
    const name = l.name || l.Name || l.fullName || l.FullName || 'Unknown';
    const contact = l.contact || l.Contact || l.contactNumber || l.ContactNumber || 'N/A';
    const email = l.email || l.Email || 'N/A';

    return `
      <tr>
        <td><strong>${id}</strong></td>
        <td>${name}</td>
        <td>${contact}</td>
        <td>${email}</td>
        <td><button class="btn-danger" onclick="deleteLearner('${id}')"><i class="fa-solid fa-trash"></i></button></td>
      </tr>
    `;
  }).join('');
}

function renderOptions() {
  const learnerOpts = '<option value="">-- Choose Learner --</option>' + 
    state.learners.map(l => `<option value="${l.id}">${l.name} (${l.id})</option>`).join('');

  const courseOpts = '<option value="">-- Choose Course --</option>' + 
    state.courses.map(c => `<option value="${c.code}">${c.title} - $${c.fee}</option>`).join('');

  elements.calcLearner.innerHTML = learnerOpts;
  elements.payLearner.innerHTML = learnerOpts;
  elements.calcCourse.innerHTML = courseOpts;
}

function renderReceipts() {
  const container = document.getElementById('receiptsList');
  if (state.payments.length === 0) {
    container.innerHTML = '<p class="text-center">No receipts recorded yet.</p>';
    return;
  }

  container.innerHTML = state.payments.slice().reverse().map(p => `
    <div class="receipt-card">
      <div style="display:flex; justify-content:space-between;">
        <strong>${p.id}</strong>
        <span style="color:#10b981;">$${p.amount.toFixed(2)}</span>
      </div>
      <div>Paid by: ${p.learnerName}</div>
      <div style="font-size:0.8rem; color:#888;">Method: ${p.method} | Date: ${p.date}</div>
    </div>
  `).join('');
}

// Global Delete Triggers
window.deleteCourse = async (code) => {
  if (state.isOnline) {
    try {
      await fetch(`${state.apiUrl}/courses/${code}`, { method: 'DELETE' });
      logCli(`API DELETE Success: ${code}`, 'success');
    } catch (err) {
      logCli(`API DELETE Failed: ${err.message}`, 'error');
    }
  }
  state.courses = state.courses.filter(c => c.code !== code);
  saveToLocalStorage();
  renderAll();
  showToast(`Course ${code} removed.`);
};

window.deleteLearner = async (id) => {
  if (state.isOnline) {
    try {
      await fetch(`${state.apiUrl}/learners/${id}`, { method: 'DELETE' });
      logCli(`API DELETE Success: ${id}`, 'success');
    } catch (err) {
      logCli(`API DELETE Failed: ${err.message}`, 'error');
    }
  }
  state.learners = state.learners.filter(l => l.id !== id);
  saveToLocalStorage();
  renderAll();
  showToast(`Learner ${id} removed.`);
};