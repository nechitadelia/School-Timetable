const form = document.getElementById("deleteProfessorform");
const deleteButton = document.getElementById("deleteProfessorButton");
let firstName = document.getElementById("firstName");
let lastName = document.getElementById("lastName");

//showing an alert message when the form is submitted - delete professor
deleteButton.addEventListener("click", () => {
    let firstNameValue = firstName.value;
    let lastNameValue = lastName.value;

    setTimeout(() => {
        form.submit();
    }, 1700);

    swal({
        title: "Success!",
        text: "Professor " + firstNameValue + " " + lastNameValue + " has been deleted!",
        icon: "success",
    });
});