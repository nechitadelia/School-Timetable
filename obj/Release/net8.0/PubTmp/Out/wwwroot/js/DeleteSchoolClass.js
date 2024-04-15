const form = document.getElementById("deleteClassform");
const submit = document.getElementById("deleteClassButton");
let classLetter = document.getElementById("ClassLetter");
let yearOfStudy = document.getElementById("YearOfStudy");

//showing an alert message when the form is submitted
submit.addEventListener("click", () => {
    let yearValue = yearOfStudy.options[yearOfStudy.selectedIndex].text;
    let classLetterValue = classLetter.options[classLetter.selectedIndex].text;

    if (classLetterValue.length == 1)
    {
        Swal.fire({
            title: "Are you sure?",
            text: "You won't be able to revert this!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Yes"
        }).then((result) => {
            if (result.isConfirmed) {
                Swal.fire({
                    title: "Deleted!",
                    text: "The class " + yearValue + classLetterValue + " has been deleted!",
                    icon: "success"
                });

                setTimeout(() => {
                    form.submit();
                }, 1700);
            }
            else {
                Swal.fire({
                    title: "Cancelled",
                    text: "The class was not deleted!",
                    icon: "error"
                });
            }
        });
    }
});

//changing the class letter depending on user input for school year
yearOfStudy.addEventListener('change', changeLetter);
function changeLetter() {
    let yearValue = yearOfStudy.value;
    classLetter.value = yearValue;
}