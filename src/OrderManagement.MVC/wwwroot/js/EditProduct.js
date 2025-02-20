document.addEventListener("DOMContentLoaded", loadProduct);

function loadProduct() {
    const productId = new URLSearchParams(window.location.search).get("id");
    if (productId) {
        fetch(`/Product/getProductById/${productId}`)
            .then(response => response.json())
            .then(product => {
                if (product) {
                    document.getElementById("product-id").value = product.productId;
                    document.getElementById("product-code").value = product.code;
                    document.getElementById("product-name").value = product.name;
                    document.getElementById("product-price").value = product.price;
                } else {
                    alert("Продукт не знайдено.");
                }
            })
            .catch(error => console.error('Error fetching product:', error));
    }
}

function updateProduct() {
    const id = document.getElementById("product-id").value;
    const code = document.getElementById("product-code").value;
    const name = document.getElementById("product-name").value;
    const price = parseFloat(document.getElementById("product-price").value);

    const productData = {
        productId: id,
        code: code,
        name: name,
        price: price
    };

    fetch('/Product/updateProduct', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(productData)
    })
        .then(response => response.json())
        .then(data => {
            if (data) {
                alert("Продукт оновлено!");
                window.location.href = "/Product/Index";
            } else {
                alert("Помилка при оновленні продукту.");
            }
        })
        .catch(error => console.error('Error updating product:', error));
}
