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
                label.title = product.name; 

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

                div.addEventListener("click", (event) => {
                    if (!event.target.classList.contains("product-amount") &&
                        !event.target.classList.contains("product-checkbox")) {
                        checkbox.checked = !checkbox.checked;
                    }
                });

                checkbox.addEventListener("click", (event) => {
                    event.stopPropagation(); 
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
        .catch(error => console.error("Помилка створення замовлення:", error));
}





function showErrorPopup(errors) {
    const popup = document.getElementById("error-popup");
    popup.innerHTML = "<ul>" + Object.values(errors).flat().map(err => `<li>${err}</li>`).join("") + "</ul>";
    popup.style.display = "block";
    setTimeout(() => popup.style.display = "none", 5000);
}