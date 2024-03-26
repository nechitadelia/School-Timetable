// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const submit = document.getElementById("send");

submit.addEventListener('click', () => {

    //e.preventDefault();

    //setTimeout(() => {
    //    window.location.reload();
    //}, 2000);

    const success = document.getElementById("success");

    success.style.display = "block";

    setTimeout(() => {
        success.style.display = "none";
    }, 2000);

    //setTimeout(() => {
    //    window.location.replace("Create.cshtml");
    //}, 2000);
   
});