let products = new Map();

products.set("rice", 60);
products.set("sugar", 50);
products.set("oil", 120);
products.set("milk", 30);
products.set("bread", 40);

let grandTotal = 0;

function generateBill(event)
{

    event.preventDefault();

    let product = document.getElementById("product").value;

    let quantity = Number(document.getElementById("quantity").value);

    let price = products.get(product);

    let total = price * quantity;

    grandTotal = grandTotal + total;

    document.getElementById("result").innerHTML +=

        product.toUpperCase() +

        " - ₹" + price +

        " × " + quantity +

        " = ₹" + total +

        "<br>";

    document.getElementById("result").innerHTML +=

        "<br><b>Grand Total: ₹" +

        grandTotal +

        "</b><br><br>";


    // Clear quantity

    document.getElementById("quantity").value = "";
}
