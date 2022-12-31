import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Book } from '../types/book';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BookService {

  constructor(private httpClient: HttpClient) {
  }

  get(id: string): Observable<Book> {
    return this.httpClient.get<Book>(`${environment.keepTrackApiUrl}/api/books/${id}`);
  }

  list(): Observable<Array<Book>> {
    return this.httpClient.get<Array<Book>>(`${environment.keepTrackApiUrl}/api/books`);
  }

  create(input: Book): Observable<Book> {
    return this.httpClient.post<Book>(`${environment.keepTrackApiUrl}/api/books`, input);
  }

  update(input: Book): Observable<number> {
    if (!input.finishedAt) {
      input.finishedAt = null;
    }
    return this.httpClient.put<number>(`${environment.keepTrackApiUrl}/api/books/${input.id}`, input);
  }

  delete(input: Book): Observable<number> {
    return this.httpClient.delete<number>(`${environment.keepTrackApiUrl}/api/books/${input.id}`);
  }

}
