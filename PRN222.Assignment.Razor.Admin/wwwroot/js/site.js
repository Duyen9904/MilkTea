//"use strict";

//console.log("Site.js loaded - starting SignalR setup " + new Date().toISOString());

//// SignalR connection setup
//var connection = new signalR.HubConnectionBuilder()
//    .withUrl("/orderHub")
//    .build();

//// DOM Elements
//const notificationContainer = document.getElementById("notification-container") ||
//    (() => {
//        console.log("Creating notification container");
//        const container = document.createElement("div");
//        container.id = "notification-container";
//        container.className = "position-fixed top-0 end-0 p-3";
//        container.style.zIndex = "1050";
//        document.body.appendChild(container);
//        return container;
//    })();

//// Add this to check if Bootstrap is available
//console.log("Bootstrap available?", typeof bootstrap !== 'undefined');

//// Handler for receiving order created notifications
//connection.on("ReceiveOrderNotification", function (message) {
//    console.log("Notification received:", message, new Date().toISOString());

//    // Create and show notification
//    showNotification(message, "New Order Created");

//    // Refresh the table
//    setTimeout(function () {
//        console.log("Reloading page...");
//        window.location.reload();
//    }, 1000);
//});

//// Generic notification display function
//function showNotification(message, title = "Notification") {
//    console.log("Showing notification:", message);

//    // Create toast element
//    const toastId = "toast-" + Date.now();
//    const toast = document.createElement("div");
//    toast.className = "toast";
//    toast.id = toastId;
//    toast.setAttribute("role", "alert");
//    toast.setAttribute("aria-live", "assertive");
//    toast.setAttribute("aria-atomic", "true");

//    // Create toast content
//    toast.innerHTML = `
//        <div class="toast-header bg-primary text-white">
//            <strong class="me-auto">${title}</strong>
//            <small>${new Date().toLocaleTimeString()}</small>
//            <button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>
//        </div>
//        <div class="toast-body">
//            ${message}
//        </div>
//    `;

//    // Add to notification container
//    notificationContainer.appendChild(toast);

//    // Try to use bootstrap Toast
//    if (typeof bootstrap !== 'undefined' && bootstrap.Toast) {
//        try {
//            const bsToast = new bootstrap.Toast(toast, {
//                autohide: true,
//                delay: 5000
//            });
//            bsToast.show();
//        } catch (e) {
//            console.error("Bootstrap Toast error:", e);
//            // Fallback for toast display
//            toast.style.display = "block";
//        }
//    } else {
//        console.warn("Bootstrap not available, showing basic toast");
//        toast.style.display = "block";
//    }

//    // Remove from DOM after hiding
//    toast.addEventListener('hidden.bs.toast', function () {
//        toast.remove();
//    });
//}

//// Function to refresh the order table without full page reload
//function refreshOrderTable() {
//    console.log("Refreshing order table");
//    fetch(window.location.href)
//        .then(response => response.text())
//        .then(html => {
//            const parser = new DOMParser();
//            const doc = parser.parseFromString(html, 'text/html');
//            const newTable = doc.querySelector('.table');
//            const currentTable = document.querySelector('.table');

//            if (newTable && currentTable) {
//                currentTable.innerHTML = newTable.innerHTML;
//                console.log("Table refreshed successfully");
//            } else {
//                console.warn("Could not find table elements");
//            }

//            // Also update pagination if it exists
//            const newPagination = doc.querySelector('.pagination');
//            const currentPagination = document.querySelector('.pagination');

//            if (newPagination && currentPagination) {
//                currentPagination.innerHTML = newPagination.innerHTML;
//            }
//        })
//        .catch(error => {
//            console.error("Error refreshing table:", error);
//        });
//}

//// Start the connection
//function startConnection() {
//    console.log("Starting SignalR connection...");
//    connection.start()
//        .then(() => {
//            console.log("SignalR Connected successfully with ID:", connection.connectionId);
//            console.log("Connection state:", connection.state);
//        })
//        .catch(err => {
//            console.error("SignalR Connection Error: ", err);
//            // Try to reconnect after 5 seconds
//            setTimeout(startConnection, 5000);
//        });
//}

//// Reconnect if connection is lost
//connection.onclose(async () => {
//    console.log("SignalR Connection Closed. Attempting to reconnect...");
//    setTimeout(startConnection, 5000);
//});

//// Initialize on page load
//document.addEventListener('DOMContentLoaded', function () {
//    console.log("DOM loaded, initializing SignalR");
//    startConnection();

//    // Add manual refresh button for testing
//    if (!document.getElementById('refresh-button')) {
//        const refreshSection = document.createElement('div');
//        refreshSection.className = 'd-flex justify-content-between align-items-center mb-3';
//        refreshSection.innerHTML = `
//            <button id="refresh-button" class="btn btn-sm btn-outline-primary">
//                <i class="bi bi-arrow-clockwise"></i> Refresh Orders
//            </button>
//        `;

//        // Insert before the table
//        const table = document.querySelector('.table');
//        if (table && table.parentNode) {
//            table.parentNode.insertBefore(refreshSection, table);
//        }
//    }

//    // Optional: Add a manual refresh button
//    const refreshButton = document.getElementById('refresh-button');
//    if (refreshButton) {
//        refreshButton.addEventListener('click', function () {
//            refreshOrderTable();
//        });
//    } else {
//        console.warn("Refresh button not found");
//    }
//});


"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/orderHub").build();


// Listen for delete completion and redirect
connection.on("ReceiveOrderNotification", function () {
    location.href = '/Order';
});

// Start the connection
connection.start()
    .then(() => console.log("SignalR Connected"))
    .catch(err => console.error("Connection Error: ", err));
