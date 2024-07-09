class Book {
    Title: string;
    AuthorName: string;
    AuthorSurname: string;
    PublishingHouse: string;

    constructor(title: string, authorName: string, authorSurname: string, publishingHouse: string) {
        this.Title = title;
        this.AuthorName = authorName;
        this.AuthorSurname = authorSurname;
        this.PublishingHouse = publishingHouse;
    }
}

const button = document.getElementById("search");

button?.addEventListener("click", EventPost);

function RecuperaValoriForm():Book {
    var Title = (document.getElementById("Title") as HTMLInputElement).value;
    var AuthorName = (document.getElementById("AuthorName") as HTMLInputElement).value;
    var AuthorSurname = (document.getElementById("AuthorSurname") as HTMLInputElement).value;
    var PublishingHouse = (document.getElementById("PublishingHouse") as HTMLInputElement).value;

    var book = new Book(Title,AuthorName,AuthorSurname,PublishingHouse);

    return book
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
    }).then(response => {
        if (!response.ok) {
            throw new Error('Network response was not ok ' + response.statusText);
        }
        return response.text();
    }).then(response => {
        const searchBookContainer = document.getElementById('booksContainer');
        if (searchBookContainer) {
            searchBookContainer.innerHTML = response;
        }
        return response;
    }).catch(error => {
        console.error(error);
    });
}



