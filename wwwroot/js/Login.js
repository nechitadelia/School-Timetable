const email = document.getElementById("EmailAddress");
const password = document.getElementById("Password");
const admin = document.getElementById("admin-login");
const user1 = document.getElementById("user1-login");
const user2 = document.getElementById("user2-login");

admin.addEventListener("click", () => {
    email.value = "admin@gmail.com";
    password.value = "Password13445556#";
});

user1.addEventListener("click", () => {
    email.value = "user1@gmail.com";
    password.value = "Password123#";
});

user2.addEventListener("click", () => {
    email.value = "user2@gmail.com";
    password.value = "Password12345#";
});