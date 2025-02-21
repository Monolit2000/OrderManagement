

//document.addEventListener("DOMContentLoaded", loadProducts);

//function loadProducts() {
//    fetch("http://localhost:5000/Product/getAllProducts")
//        .then(response => response.json())
//        .then(products => {
//            let container = document.getElementById("products-list");
//            products.forEach(product => {
//                let div = document.createElement("div");
//                div.innerHTML = `
//                    <input type="checkbox" class="product-checkbox" data-id="${product.productId}" data-name="${product.name}">
//                    <label>${product.name} (${product.price} грн)</label>
//                    <input type="number" class="product-amount" min="1" value="1">
//                `;
//                container.appendChild(div);
//            });
//        })
//        .catch(error => console.error("Error loading products:", error));
//}




//document.addEventListener("DOMContentLoaded", loadProducts);

//function loadProducts() {
//    fetch("http://localhost:5000/Product/getAllProducts")
//        .then(response => response.json())
//        .then(products => {
//            let container = document.getElementById("products-list");
//            products.forEach(product => {
//                let div = document.createElement("div");
//                div.classList.add("product-item");
//                div.innerHTML = `
//                    <input type="checkbox" class="product-checkbox" data-id="${product.productId}" data-name="${product.name}">
//                    <label>${product.name} <strong>(${product.price} грн)</strong></label>
//                    <input type="number" class="product-amount" min="1" value="1">
//                `;
//                container.appendChild(div);
//            });
//        })
//        .catch(error => console.error("Error loading products:", error));
//}


//document.addEventListener("DOMContentLoaded", loadProducts);

//function loadProducts() {
//    fetch("http://localhost:5000/Product/getAllProducts")
//        .then(response => response.json())
//        .then(products => {
//            let container = document.getElementById("products-list");
//            products.forEach(product => {
//                let div = document.createElement("div");
//                div.classList.add("product-item");
//                div.innerHTML = `
//                    <input type="checkbox" class="product-checkbox" data-id="${product.productId}" data-name="${product.name}">
//                    <label class="product-name">${product.name}</label>
//                    <span class="product-price">${product.price} грн</span>
//                    <input type="number" class="product-amount" min="1" value="1">
//                `;
//                container.appendChild(div);
//            });
//        })
//        .catch(error => console.error("Error loading products:", error));
//}



//document.addEventListener("DOMContentLoaded", loadProducts);

//function loadProducts() {
//    fetch("http://localhost:5000/Product/getAllProducts")
//        .then(response => response.json())
//        .then(products => {
//            let container = document.getElementById("products-list");
//            products.forEach(product => {
//                let div = document.createElement("div");
//                div.classList.add("product-item");

//                let label = document.createElement("label");
//                label.classList.add("product-name");
//                label.textContent = product.name;
//                label.title = product.name; // Tooltip при наведении

//                div.innerHTML = `
//                    <input type="checkbox" class="product-checkbox" data-id="${product.productId}" data-name="${product.name}">
//                    <span class="product-price">${product.price} грн</span>
//                    <input type="number" class="product-amount" min="1" value="1">
//                `;

//                div.insertBefore(label, div.firstChild); // Вставляем название перед чекбоксом
//                container.appendChild(div);
//            });
//        })
//        .catch(error => console.error("Error loading products:", error));
//}

//document.addEventListener("DOMContentLoaded", loadProducts);

//function loadProducts() {
//    fetch("http://localhost:5000/Product/getAllProducts")
//        .then(response => response.json())
//        .then(products => {
//            let container = document.getElementById("products-list");
//            products.forEach(product => {
//                let div = document.createElement("div");
//                div.classList.add("product-item");

//                let checkbox = document.createElement("input");
//                checkbox.type = "checkbox";
//                checkbox.classList.add("product-checkbox");
//                checkbox.dataset.id = product.productId;
//                checkbox.dataset.name = product.name;

//                let label = document.createElement("label");
//                label.classList.add("product-name");
//                label.textContent = product.name;
//                label.title = product.name; // Tooltip при наведении

//                let price = document.createElement("span");
//                price.classList.add("product-price");
//                price.textContent = `${product.price} грн`;

//                let amount = document.createElement("input");
//                amount.type = "number";
//                amount.classList.add("product-amount");
//                amount.min = 1;
//                amount.value = 1;

//                div.appendChild(checkbox);
//                div.appendChild(label);
//                div.appendChild(price);
//                div.appendChild(amount);
//                container.appendChild(div);

//                // 📌 Добавляем обработчик клика на всю карточку
//                div.addEventListener("click", (event) => {
//                    if (!event.target.classList.contains("product-amount")) {
//                        checkbox.checked = !checkbox.checked;
//                    }
//                });
//            });
//        })
//        .catch(error => console.error("Error loading products:", error));
//}

document.addEventListener("DOMContentLoaded", loadProducts);

function loadProducts() {
    fetch("http://localhost:5000/Product/getAllProducts")
        .then(response => response.json())
        .then(products => {
            let container = document.getElementById("products-list");
            products.forEach(product => {
                let div = document.createElement("div");
                div.classList.add("product-item");

                let checkbox = document.createElement("input");
                checkbox.type = "checkbox";
                checkbox.classList.add("product-checkbox");
                checkbox.dataset.id = product.productId;
                checkbox.dataset.name = product.name;

                let label = document.createElement("label");
                label.classList.add("product-name");
                label.textContent = product.name;
                label.title = product.name; // Tooltip при наведении

                let price = document.createElement("span");
                price.classList.add("product-price");
                price.textContent = `${product.price} грн`;

                let amount = document.createElement("input");
                amount.type = "number";
                amount.classList.add("product-amount");
                amount.min = 1;
                amount.value = 1;

                div.appendChild(checkbox);
                div.appendChild(label);
                div.appendChild(price);
                div.appendChild(amount);
                container.appendChild(div);

                // 📌 Добавляем обработчик клика на карточку
                div.addEventListener("click", (event) => {
                    if (!event.target.classList.contains("product-amount") &&
                        !event.target.classList.contains("product-checkbox")) {
                        checkbox.checked = !checkbox.checked;
                    }
                });

                // 📌 Исправлено: при клике на чекбокс он работает нормально
                checkbox.addEventListener("click", (event) => {
                    event.stopPropagation(); // Останавливаем всплытие, чтобы клик не срабатывал на карточке
                });
            });
        })
        .catch(error => console.error("Error loading products:", error));
}


function createOrder() {
    let customerName = document.getElementById("customer-name").value;
    let customerPhone = document.getElementById("customer-phone").value;
    let selectedProducts = [];

    document.querySelectorAll(".product-checkbox:checked").forEach(checkbox => {
        let amount = checkbox.parentElement.querySelector(".product-amount").value;
        selectedProducts.push({
            productId: checkbox.dataset.id,
            productName: checkbox.dataset.name,
            amount: parseInt(amount, 10)
        });
    });

    let orderData = {
        customerFullName: customerName,
        customerPhone: customerPhone,
        orderProducts: selectedProducts
    };

    fetch("http://localhost:5000/Order/createOrder", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(orderData)
    })
        .then(response => response.json())
        .then(data => {
            //alert("Замовлення створено!");
            //window.location.href = "/Home/Index";
        })
        .catch(error => console.error("Error creating order:", error));
}
