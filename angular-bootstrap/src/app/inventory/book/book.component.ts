import { Component, OnInit, OnDestroy, ElementRef, ViewChild } from '@angular/core';
import { Subscription } from 'rxjs';
import { BookService } from 'src/app/backend/services/book.service';
import { Book } from 'src/app/backend/types/book';
import { AuthenticateService } from 'src/app/user/services/authenticate.service';

@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.css']
})
export class BookComponent implements OnInit, OnDestroy {

  @ViewChild('titleInput') titleInput: ElementRef;

  books: Array<Book> = [];
  userEventsSubscription: Subscription;

  constructor(private bookService: BookService, private authenticateService: AuthenticateService) { }

  ngOnInit() {
    this.userEventsSubscription = this.authenticateService.auth.user.subscribe(user => {
      this.bookService.list()
        .subscribe(
          books => {
            this.books = books;
          },
          error => console.warn(error)
        );
      });
  }

  ngOnDestroy() {
    if (this.userEventsSubscription) {
      this.userEventsSubscription.unsubscribe();
    }
  }

  create(title: string) {
    this.bookService.create({ title }).subscribe(book => {
      this.books.push(book);
      this.titleInput.nativeElement.value = '';
    });
  }

  startEditing(book: Book) {
    book.isEditable = true;
  }

  cancel(book: Book) {
    book.isEditable = false;
    this.bookService.get(book.id).subscribe(item => {
      book.title = item.title;
      book.author = item.author;
      book.series = item.series;
      book.finishedAt = item.finishedAt;
    });
  }

  update(book: Book) {
    this.bookService.update(book).subscribe(updatedCount => book.isEditable = false);
  }

  delete(book: Book) {
    this.bookService.delete(book).subscribe(deletedCount => this.books.splice(this.books.findIndex(x => x.id === book.id), 1));
  }

}
