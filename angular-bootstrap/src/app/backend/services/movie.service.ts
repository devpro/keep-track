import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from 'src/environments/environment';
import { Movie } from '../types/movie';

@Injectable({
  providedIn: 'root'
})
export class MovieService {

  constructor(private httpClient: HttpClient) {
  }

  list(search?: string): Observable<Array<Movie>> {
    return this.httpClient.get<Array<Movie>>(`${environment.keepTrackApiUrl}/api/movies?search=${search ?? ''}&page=0&pageSize=50`);
  }

  create(input: Movie): Observable<Movie> {
    return this.httpClient.post<Movie>(`${environment.keepTrackApiUrl}/api/movies`, input);
  }

  delete(input: Movie): Observable<number> {
    return this.httpClient.delete<number>(`${environment.keepTrackApiUrl}/api/movies/${input.id}`);
  }

}
