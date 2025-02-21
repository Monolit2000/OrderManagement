//document.addEventListener("DOMContentLoaded", loadOrders);

//function loadOrders() {
//    fetch('http://localhost:5000/Order/getAllOrders')
//        .then(response => response.json())
//        .then(orders => {
//            const list = document.getElementById("order-list");
//            if (!list) return;
//            list.innerHTML = "";

//            orders.forEach(order => {
//                let li = document.createElement("li");
//                li.textContent = `#${order.orderId} ${order.customerFullName} (${order.customerPhone})`;

//                // Создаем вложенный список для orderProducts
//                let productList = document.createElement("ul");

//                order.orderProducts.forEach(orderProduct => {
//                    let li2 = document.createElement("li");
//                    li2.textContent = `${orderProduct.productName}`;
//                    productList.appendChild(li2); // Добавляем продукт в вложенный список
//                });

//                li.appendChild(productList); // Добавляем вложенный список в основной элемент заказа
//                list.appendChild(li); // Добавляем заказ в основной список
//            });
//        })
//        .catch(error => console.error('Error fetching orders:', error));
//}


document.addEventListener("DOMContentLoaded", loadOrders);

function loadOrders() {
    fetch('http://localhost:5000/Order/getAllOrders')
        .then(response => response.json())
        .then(orders => {
            const list = document.getElementById("order-list");
            if (!list) return;
            list.innerHTML = "";

            orders.forEach(order => {
                let orderItem = document.createElement("div");
                orderItem.classList.add("order-item");

                // Основная информация о заказе
                let orderHeader = document.createElement("div");
                orderHeader.classList.add("order-header");
                orderHeader.textContent = `#
                ${order.orderId} 
                ${order.customerFullName} 
                (${order.customerPhone})`;

                // Создаем выпадающий список товаров
                let details = document.createElement("details");
                let summary = document.createElement("summary");

                // Подсчитываем количество товаров
                const productCount = order.orderProducts.length;
                summary.textContent = `🛒 Перелік товарів (${productCount})`; // Добавляем количество товаров

                let productList = document.createElement("ul");
                productList.classList.add("product-list");

                order.orderProducts.forEach(orderProduct => {
                    let productItem = document.createElement("li");
                    productItem.textContent = orderProduct.productName;
                    productList.appendChild(productItem);
                });

                details.appendChild(summary);
                details.appendChild(productList);

                orderItem.appendChild(orderHeader);
                orderItem.appendChild(details);
                list.appendChild(orderItem);
            });
        })
        .catch(error => console.error('Error fetching orders:', error));
}
