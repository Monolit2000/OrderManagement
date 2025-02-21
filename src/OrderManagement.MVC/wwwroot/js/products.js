



document.addEventListener("DOMContentLoaded", loadProducts);

let allProducts = [];

function loadProducts() {
    fetch('http://localhost:5000/Product/getAllProducts')
        .then(response => response.json())
        .then(products => {
            allProducts = products;
            displayProducts(allProducts);
        })
        .catch(error => console.error('Error fetching products:', error));
}

function displayProducts(products) {
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
}

document.getElementById("search-btn-code").addEventListener("click", searchProductsByCode);
document.getElementById("search-btn-id").addEventListener("click", searchProductsById);

function searchProductsByCode() {
    const searchValue = document.getElementById("search-input-by-code").value;
    if (searchValue.trim() === "") {
        displayProducts(allProducts); 
        return;
    }

    fetch(`http://localhost:5000/Product/GetProductsByCode/${searchValue}`)
        .then(response => {
            if (response.ok) {
                return response.json();
            } else {
                throw new Error('Product not found');
            }
        })
        .then(product => {
            displayProducts([product]); 
        })
        .catch(error => {
            console.error('Error fetching product by ID:', error);
            displayProducts([]); 
        });
}

function searchProductsById() {
    const searchValue = document.getElementById("search-input-by-Id").value;
    if (searchValue.trim() === "") {
        displayProducts(allProducts); 
        return;
    }

    fetch(`http://localhost:5000/Product/getProductById/${searchValue}`)
        .then(response => {
            if (response.ok) {
                return response.json();
            } else {
                throw new Error('Product not found');
            }
        })
        .then(product => {
            displayProducts([product]); 
        })
        .catch(error => {
            console.error('Error fetching product by ID:', error);
            displayProducts([]); 
        });
}

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
                loadProducts();
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





