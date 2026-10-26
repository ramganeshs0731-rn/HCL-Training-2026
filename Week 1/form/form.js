let students = new Map();

function addStudent(event)
{
    // Prevent page refresh

    event.preventDefault();

    let name = document.getElementById("name").value;

    let age = document.getElementById("age").value;

    let email = document.getElementById("email").value;

    let department = document.getElementById("department").value;

    let student = {

        name: name,

        age: age,

        email: email,

        department: department

    };

    students.set(email, student);

    document.getElementById("result").innerHTML =
        "Student Added Successfully!<br><br>" +

        "Name: " + student.name + "<br>" +

        "Age: " + student.age + "<br>" +

        "Email: " + student.email + "<br>" +

        "Department: " + student.department;

    document.getElementById("name").value = "";

    document.getElementById("age").value = "";

    document.getElementById("email").value = "";

    document.getElementById("department").value = "";
}
