document.addEventListener("DOMContentLoaded", loadProducts);

function loadProducts() {
    fetch('http://localhost:5000/Product/getAllProducts')
        .then(response => response.json())
        .then(products => {
            const list = document.getElementById("product-list");
            if (!list) return;
            list.innerHTML = "";

            products.forEach(product => {
                let li = document.createElement("li");
                li.classList.add("product-item");

                let nameInput = document.createElement("input");
                nameInput.type = "text";
                nameInput.value = product.name;
                nameInput.classList.add("product-name");

                let codeInput = document.createElement("input");
                codeInput.type = "text";
                codeInput.value = product.code;
                codeInput.classList.add("product-code");

                let priceInput = document.createElement("input");
                priceInput.type = "number";
                priceInput.value = product.price;
                priceInput.classList.add("product-price");

                let updateButton = document.createElement("button");
                updateButton.textContent = "🔄 Оновити";
                updateButton.onclick = () => updateProduct(product.productId, nameInput.value, codeInput.value, priceInput.value);

                li.appendChild(nameInput);
                li.appendChild(codeInput);
                li.appendChild(priceInput);
                li.appendChild(updateButton);

                list.appendChild(li);
            });
        })
        .catch(error => console.error('Error fetching products:', error));
}

//function createProduct() {
//    const codeInput = document.getElementById("product-code");
//    const nameInput = document.getElementById("product-name");
//    const priceInput = document.getElementById("product-price");

//    const code = codeInput.value;
//    const name = nameInput.value;
//    const price = parseFloat(priceInput.value);

//    const productData = { code, name, price };

//    fetch('http://localhost:5000/Product/createProduct', {
//        method: 'POST',
//        headers: { 'Content-Type': 'application/json' },
//        body: JSON.stringify(productData)
//    })
//        .then(response => response.json())
//        .then(data => {
//            if (data) {
//                alert("Продукт створено!");
//                loadProducts();

//                codeInput.value = '';
//                nameInput.value = '';
//                priceInput.value = '';
//            } else {
//                alert("Помилка при створенні продукту.");
//            }
//        })
//        .catch(error => console.error('Error creating product:', error));
//}

//function updateProduct(productId, name, code, price) {
//    const updatedData = { productId, name, code, price: parseFloat(price) };

//    fetch(`http://localhost:5000/Product/updateProduct`, {
//        method: 'PUT',
//        headers: { 'Content-Type': 'application/json' },
//        body: JSON.stringify(updatedData)
//    })
//        .then(response => response.json())
//        .then(data => {
//            if (data) {
//                alert("Продукт оновлено!");
//            } else {
//                alert("Помилка при оновленні продукту.");
//            }
//        })
//        .catch(error => console.error('Error updating product:', error));
//}


function createProduct() {
    const codeInput = document.getElementById("product-code");
    const nameInput = document.getElementById("product-name");
    const priceInput = document.getElementById("product-price");

    const productData = {
        code: codeInput.value,
        name: nameInput.value,
        price: parseFloat(priceInput.value)
    };

    fetch('http://localhost:5000/Product/createProduct', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(productData)
    })
        .then(response => response.json().then(data => ({ status: response.status, body: data })))
        .then(({ status, body }) => {
            if (status === 400) {
                showErrorPopup(body.errors);
            } else if (status === 500) {
                showErrorPopup({ code: [body.detail] });
            } else {
                alert("Продукт створено!");
                loadProducts();

                codeInput.value = '';
                nameInput.value = '';
                priceInput.value = '';
            }
        })
        .catch(error => console.error('Error creating product:', error));
}

function updateProduct(productId, name, code, price) {
    const updatedData = { productId, name, code, price: parseFloat(price) };

    fetch(`http://localhost:5000/Product/updateProduct`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(updatedData)
    })
        .then(response => response.json().then(data => ({ status: response.status, body: data })))
        .then(({ status, body }) => {
            if (status === 400) {
                showErrorPopup(body.errors);
            } else if (status === 500) {
                showErrorPopup({ code: [body.detail] });
            } else {
                alert("Продукт оновлено!");
            }
        })
        .catch(error => console.error('Error updating product:', error));
}


function showErrorPopup(errors) {
    const popup = document.getElementById("error-popup");
    popup.innerHTML = "<ul>" + Object.values(errors).flat().map(err => `<li>${err}</li>`).join("") + "</ul>";
    popup.style.display = "block";
    setTimeout(() => popup.style.display = "none", 5000);
}
