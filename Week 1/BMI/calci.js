function func()
{
    let weight = Number(document.getElementById("weight").value)
    let height = Number(document.getElementById("height").value)

    height = height / 100

    let bmi = weight / (height * height)

    if(bmi < 18.5)
    {
        document.getElementById("res").innerHTML =
        "Your BMI is " + bmi.toFixed(2) + " - Underweight"
    }
    else if(bmi < 25)
    {
        document.getElementById("res").innerHTML =
        "Your BMI is " + bmi.toFixed(2) + " - Normal"
    }
    else if(bmi < 30)
    {
        document.getElementById("res").innerHTML =
        "Your BMI is " + bmi.toFixed(2) + " - Overweight"
    }
    else
    {
        document.getElementById("res").innerHTML =
        "Your BMI is " + bmi.toFixed(2) + " - Obesity"
    }
}