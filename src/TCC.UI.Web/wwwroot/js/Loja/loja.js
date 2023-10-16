const comprar1 = document.getElementById('comprar');

const modal = document.getElementById('myModal');
const modal2 = document.getElementById('myModal2');
const confirmButton = document.getElementById('confirmButton');
const cancelButton = document.getElementById('cancelButton');

comprar1.addEventListener('click', function (e) {
    e.preventDefault();
    modal.style.display = 'flex';
});

comprar1.addEventListener('click', function (e) {
    e.preventDefault();
    modal2.style.display = 'flex';
});

confirmButton.addEventListener('click', function () {
    alert('Compra realizada com sucesso!');
    modal.style.display = 'none';
});
cancelButton.addEventListener('click', function () {
    alert('Compra cancelada.');
    modal.style.display = 'none';
});