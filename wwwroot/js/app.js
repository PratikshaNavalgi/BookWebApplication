document.getElementById('getBooks').addEventListener('click', () => fetchBooks(false));
document.getElementById('getHardcover').addEventListener('click', () => fetchBooks(true));

function fetchBooks(hardcoverOnly) {
    const adultBooksUl = document.getElementById('adultBooks');
    const childrenBooksUl = document.getElementById('childrenBooks');
    const adultHeading = document.getElementById('adultHeading');
    const childrenHeading = document.getElementById('childrenHeading');

    const endpoint = `https://localhost:5001/api/books?hardcoverOnly=${hardcoverOnly}`;

    fetch(endpoint)
        .then(response => response.json())
        .then(data => {
            adultBooksUl.innerHTML = '';
            childrenBooksUl.innerHTML = '';

            data.forEach(owner => {
                const list = owner.Category === 'Adults' ? adultBooksUl : childrenBooksUl;
                owner.Books.forEach(book => {
                    const listItem = document.createElement('li');
                    listItem.textContent = book.Name;
                    list.appendChild(listItem);
                });
            });

            if (hardcoverOnly) {
                adultHeading.textContent = 'Hardcover Books owned by Adults';
                childrenHeading.textContent = 'Hardcover Books owned by Children';
            } else {
                adultHeading.textContent = 'Books owned by Adults';
                childrenHeading.textContent = 'Books owned by Children';
            }
        })
        .catch(error => console.error('Error fetching data:', error));
}
