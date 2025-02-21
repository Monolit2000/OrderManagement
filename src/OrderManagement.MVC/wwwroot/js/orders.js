
document.addEventListener("DOMContentLoaded", loadOrders);

function loadOrders() {
    fetch('http://localhost:5000/Order/getAllOrders')
        .then(response => response.json())
        .then(orders => {
            displayOrders(orders);
        })
        .catch(error => console.error('Error fetching orders:', error));
}

function displayOrders(orders) {
    const list = document.getElementById("order-list");
    if (!list) return;
    list.innerHTML = "";

    orders.forEach(order => {
        let orderItem = document.createElement("div");
        orderItem.classList.add("order-item");

        let orderHeader = document.createElement("div");
        orderHeader.classList.add("order-header");
        orderHeader.textContent = `ID: ${order.orderId}, Name: ${order.customerFullName}, Phone: (${order.customerPhone})`;

        let details = document.createElement("details");
        let summary = document.createElement("summary");

        const productCount = order.orderProducts.length;
        summary.textContent = `🛒 Перелік товарів (${productCount})`;

        let productList = document.createElement("ul");
        productList.classList.add("product-list");

        order.orderProducts.forEach(orderProduct => {
            let productItem = document.createElement("li");
            productItem.textContent = `ID: ${orderProduct.productId}, Name: ${orderProduct.productName}`;
            productList.appendChild(productItem);
        });

        details.appendChild(summary);
        details.appendChild(productList);

        orderItem.appendChild(orderHeader);
        orderItem.appendChild(details);
        list.appendChild(orderItem);
    });
}

document.getElementById("search-order-btn").addEventListener("click", function () {
    const orderId = document.getElementById("order-id").value;
    if (orderId.trim() === "") {
       
        loadOrders();
    } else {
        fetch(`http://localhost:5000/Order/getOrderById/${orderId}`)
            .then(response => response.json())
            .then(order => {
                if (order) {
                    displayOrders([order]);
                } else {
                    console.warn('Order not found');
                }
            })
            .catch(error => console.error('Error fetching order:', error));
    }
});