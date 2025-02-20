document.addEventListener("DOMContentLoaded", loadProducts);

function loadProducts() {
    fetch('/Product/getAllProducts')
        .then(response => response.json())
        .then(products => {
            const list = document.getElementById("product-list");
            if (!list) return;
            list.innerHTML = "";
            products.forEach(product => {
                let li = document.createElement("li");
                li.textContent = `${product.productId} - ${product.name} (${product.code}) - Ціна: ${product.price} ₴`;
                list.appendChild(li);
            });
        })
        .catch(error => console.error('Error fetching products:', error));
}

function createProduct() {
    const code = document.getElementById("product-code").value;
    const name = document.getElementById("product-name").value;
    const price = parseFloat(document.getElementById("product-price").value);

    const productData = {
        code: code,
        name: name,
        price: price
    };

    fetch('/Product/createProduct', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(productData)
    })
        .then(response => response.json())
        .then(data => {
            if (data) {
                alert("Продукт створено!");
                window.location.href = "/Product/Index";
            } else {
                alert("Помилка при створенні продукту.");
            }
        })
        .catch(error => console.error('Error creating product:', error));
}
