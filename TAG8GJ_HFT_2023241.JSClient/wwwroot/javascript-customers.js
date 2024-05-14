let customers = [];
let connection = null;

let customerIdtoUpdate = -1;

getData();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:52322/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("customerCreated", (user, message) => {
        getData();
    });

    connection.on("customerDeleted", (user, message) => {
        getData();
    });

    connection.on("customerUpdated", (user, message) => {
        getData();
    });

    connection.onclose
        (async () => {
            await start();
        });
    start();

}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

async function getData() {
    await fetch('http://localhost:52322/Customer')
        .then(x => x.json())
        .then(y => {
            customers = y;
            console.log(customers);
            display();
        });
}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    customers.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.customerId + "</td><td>"
            + t.customerName + "</td><td>" +
        `<button type="button" onclick="remove(${t.customerId})">Delete</button>` +
        `<button type="button" onclick="showupdate(${t.customerId})">Update</button>`
            + "</td></tr>";
    });
}

function remove(customerId) {
    fetch('http://localhost:52322/customer/' + customerId, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getData();
        })
        .catch((error) => { console.error('Error:', error); });
}

function showupdate(customerId) {
    document.getElementById('customernametoupdate').value = customers.find(t => t['customerId'] == customerId)['customerName'];
    document.getElementById('updateformdiv').style.display = 'flex';
    customerIdtoUpdate = customerId;
}

function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let gottenName = document.getElementById('customernametoupdate').value;
    fetch('http://localhost:52322/customer', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
            customerId: customerIdtoUpdate,
            CustomerName: gottenName,
            CustomerEmail: 'default@example.com', 
            CustomerPhone: '1234567890'
        })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getData();
        })
        .catch((error) => { console.error('Error:', error); });
}

function create() {
    let gottenName = document.getElementById('customername').value;
    fetch('http://localhost:52322/customer', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify({
            
            CustomerName: gottenName,
            CustomerEmail: 'default@example.com', // Default email
            CustomerPhone: '1234567890' // Default phone number
        })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getData();
        })
        .catch((error) => { console.error('Error:', error); });
}