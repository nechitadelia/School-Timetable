//searchbar for users
let allUsers = document.querySelectorAll(".user-entity");
let searchInputUsers = document.querySelector("[data-users]");
let noUsers = document.querySelector(".noUsersFound");

searchInputUsers.addEventListener("input", (e) => {
    const value = e.target.value.toLowerCase();
    let checkResults = false;

    allUsers.forEach(user => {
        let userName = user.children[0].innerHTML.toLowerCase();
        let userCounty = user.children[1].innerHTML.toLowerCase();
        let userCity = user.children[2].innerHTML.toLowerCase();
        let userEmail = user.children[3].innerHTML.toLowerCase();

        const isVisible = userName.includes(value) || userCounty.includes(value) || userCity.includes(value) || userEmail.includes(value);
        user.classList.toggle("hide", !isVisible);
    });

    //display "no results" if there are no users found
    for (let i = 0; i < allUsers.length; i++) {
        if (!allUsers[i].classList.contains("hide")) {
            checkResults = true;
            i = allUsers.length - 1;
        }
        else {
            checkResults = false;
        }
    }

    if (checkResults) {
        noUsers.classList.add("hide");
    }
    else {
        noUsers.classList.remove("hide");
    }
});