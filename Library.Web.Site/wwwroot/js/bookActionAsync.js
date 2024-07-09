var Book = /** @class */ (function () {
    function Book(title, authorName, authorSurname, publishingHouse) {
        this.Title = title;
        this.AuthorName = authorName;
        this.AuthorSurname = authorSurname;
        this.PublishingHouse = publishingHouse;
    }
    return Book;
}());
var button = document.getElementById("search");
button === null || button === void 0 ? void 0 : button.addEventListener("click", EventPost);
function RecuperaValoriForm() {
    var Title = document.getElementById("Title").value;
    var AuthorName = document.getElementById("AuthorName").value;
    var AuthorSurname = document.getElementById("AuthorSurname").value;
    var PublishingHouse = document.getElementById("PublishingHouse").value;
    var book = new Book(Title, AuthorName, AuthorSurname, PublishingHouse);
    return book;
}
function EventPost(event) {
    event.preventDefault();
    var book = RecuperaValoriForm();
    fetch(("Book/IndexPost"), {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(book)
    }).then(function (response) {
        if (!response.ok) {
            throw new Error('Network response was not ok ' + response.statusText);
        }
        return response.text();
    }).then(function (response) {
        var searchBookContainer = document.getElementById('booksContainer');
        if (searchBookContainer) {
            searchBookContainer.innerHTML = response;
        }
        return response;
    }).catch(function (error) {
        console.error(error);
    });
}
//# sourceMappingURL=bookActionAsync.js.map