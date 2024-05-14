const form = document.getElementById("editUserform");
const editButton = document.getElementById("editUserButton");
let county = document.getElementById("County");
let city = document.getElementById("City");

//showing an alert message when the form is submitted - edit user info
editButton.addEventListener("click", () => {
    let countyValue = county.value;
    let cityValue = city.value;

    if (checkName(countyValue) && checkName(cityValue)) {
        setTimeout(() => {
            form.submit();
        }, 1700);

        Swal.fire({
            title: "Success!",
            text: "Your info has been updated!",
            icon: "success",
        });
    }
    else {
        form.submit();
    }
});