document.addEventListener("DOMContentLoaded", loadOrders);

function loadOrders() {
    fetch('http://localhost:5000/Order/getAllOrders')
        .then(response => response.json())
        .then(orders => {
            const list = document.getElementById("order-list");
            if (!list) return;
            list.innerHTML = "";

            orders.forEach(order => {
                let li = document.createElement("li");
                li.textContent = `#${order.orderId} ${order.customerFullName} (${order.customerPhone})`;

                // Создаем вложенный список для orderProducts
                let productList = document.createElement("ul");

                order.orderProducts.forEach(orderProduct => {
                    let li2 = document.createElement("li");
                    li2.textContent = `${orderProduct.productName}`;
                    productList.appendChild(li2); // Добавляем продукт в вложенный список
                });

                li.appendChild(productList); // Добавляем вложенный список в основной элемент заказа
                list.appendChild(li); // Добавляем заказ в основной список
            });
        })
        .catch(error => console.error('Error fetching orders:', error));
}
function createOrder() {
    const customerName = document.getElementById("customer-name").value;
    const customerPhone = document.getElementById("customer-phone").value;

    const orderData = {
        customerFullName: customerName,
        customerPhone: customerPhone,
        orderProducts: [] // Заповніть масив продуктів, якщо потрібно
    };

    fetch('http://localhost:5000/Order/createOrder', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(orderData)
    })
        .then(response => response.json())
        .then(data => {
            if (data) {
                alert("Замовлення створено!");
                window.location.href = "/Home/Index";
            } else {
                alert("Помилка при створенні замовлення.");
            }
        })
        .catch(error => console.error('Error creating order:', error));
}
