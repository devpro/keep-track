import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Movie } from '../types/movie';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MovieService {

  constructor(private httpClient: HttpClient) {
  }

  list(): Observable<Array<Movie>> {
    return this.httpClient.get<Array<Movie>>(`${environment.keepTrackApiUrl}/api/movies`);
  }

  create(input: Movie): Observable<Movie> {
    return this.httpClient.post<Movie>(`${environment.keepTrackApiUrl}/api/movies`, input);
  }
}
