async function func()
{
    let response=await fetch('text.txt')
    let text=await response.text()
    document.getElementById('RamG').innerText =text;
}

func()