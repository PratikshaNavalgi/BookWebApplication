document.getElementById('getBooks').addEventListener('click', function () {
    fetchBooks(false);
});

document.getElementById('getHardcover').addEventListener('click', function () {
    fetchBooks(true);
});

function fetchBooks(hardcoverOnly) {
    const url = `/api/books?hardcoverOnly=${hardcoverOnly}`;
    fetch(url)
        .then(response => response.json())
        .then(data => displayBooks(data, hardcoverOnly))
        .catch(error => console.error('Fetch error:', error));
}

function displayBooks(data, hardcoverOnly) {
    const adultBooks = document.getElementById('adultBooks');
    const childBooks = document.getElementById('childBooks');

    adultBooks.innerHTML = '';
    childBooks.innerHTML = '';

    const adultBooksArray = [];
    const childBooksArray = [];

    data.forEach(owner => {
        owner.Books.forEach(book => {
            if (!hardcoverOnly || book.Type === 'Hardcover') {
                const bookItem = { name: book.Name, age: owner.Age };
                if (owner.Age >= 18) {
                    adultBooksArray.push(bookItem);
                } else {
                    childBooksArray.push(bookItem);
                }
            }
        });
    });

    adultBooksArray.sort((a, b) => a.name.localeCompare(b.name));
    childBooksArray.sort((a, b) => a.name.localeCompare(b.name));

    adultBooksArray.forEach(book => {
        const li = document.createElement('li');
        li.textContent = book.name;
        adultBooks.appendChild(li);
    });

    childBooksArray.forEach(book => {
        const li = document.createElement('li');
        li.textContent = book.name;
        childBooks.appendChild(li);
    });

    // Update section headers
    document.getElementById('adultSection').querySelector('h2').textContent = hardcoverOnly ? 'Hardcover Books owned by Adults' : 'Books owned by Adults';
    document.getElementById('childSection').querySelector('h2').textContent = hardcoverOnly ? 'Hardcover Books owned by Children' : 'Books owned by Children';
}
