const form = document.getElementById("deleteUserform");
const deleteButton = document.getElementById("deleteUserButton");
let schoolName = document.getElementById("SchoolName");

//showing an alert message when the form is submitted - delete professor
deleteButton.addEventListener("click", () => {
    let schoolNameValue = schoolName.value;

    setTimeout(() => {
        form.submit();
    }, 1700);

    Swal.fire({
        title: "Success!",
        text: "User '" + schoolNameValue + "' has been deleted!",
        icon: "success",
    });
});