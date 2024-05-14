let cars = [];
let connection = null;

let carIdtoUpdate = -1;

getData();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:52322/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("carCreated", (user, message) => {
        getData();
    });

    connection.on("carDeleted", (user, message) => {
        getData();
    });

    connection.on("carUpdated", (user, message) => {
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
    await fetch('http://localhost:52322/car')
        .then(x => x.json())
        .then(y => {
            cars = y;
            //console.log(cars);
            display();
        });
}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    cars.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.carID + "</td><td>"
            + t.model + "</td><td>" +
        `<button type="button" onclick="remove(${t.carID})">Delete</button>` +
        `<button type="button" onclick="showupdate(${t.carID})">Update</button>`
            + "</td></tr>";
    });
}

function remove(carID) {
    fetch('http://localhost:52322/car/' + carID, {
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

function showupdate(carID) {
    document.getElementById('carnametoupdate').value = cars.find(t => t['carID'] == carID)['model'];
    document.getElementById('updateformdiv').style.display = 'flex';
    carIdtoUpdate = carID;
}

function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let gottenName = document.getElementById('carnametoupdate').value;
    fetch('http://localhost:52322/car', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
            carID: carIdtoUpdate,
            brand: 'Default Brand',
            model: gottenName,
            licencePlate: 'Default Plate',
            year: 2022,
            dailyRentalCost: 100
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
    let gottenName = document.getElementById('carname').value;
    fetch('http://localhost:52322/car', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify({
            
            brand: 'Default Brand',
            model: gottenName,
            licencePlate: 'Default Plate',
            year: 2022,
            dailyRentalCost: 100
        })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getData();
        })
        .catch((error) => { console.error('Error:', error); });
}