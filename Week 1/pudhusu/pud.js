function bill()
{
    let product = document.getElementById("product").value;

    let quantity = document.getElementById("quantity").value;

    let price;


    if(product == "Rice")
    {
        price = 60;
    }

    else if(product == "Sugar")
    {
        price = 50;
    }

    else if(product == "Milk")
    {
        price = 30;
    }

    else if(product == "Oil")
    {
        price = 120;
    }

    else if(product == "Bread")
    {
        price = 40;
    }


    let total = price * quantity;


    document.getElementById("res").innerHTML =

        product + " × " + quantity +

        " = ₹" + total;
}