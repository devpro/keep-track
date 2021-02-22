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

  books: Array<Book> = [];
  userEventsSubscription: Subscription;
  @ViewChild('titleInput') titleInput: ElementRef;

  constructor(private bookService: BookService, private authenticateService: AuthenticateService) { }

  ngOnInit() {
    this.userEventsSubscription = this.authenticateService.user.subscribe(user => {
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

  delete(book: Book) {
    this.bookService.delete(book).subscribe(deletedCount => this.books.splice(this.books.findIndex(x => x.id === book.id), 1));
  }

}
