import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { environment } from 'src/environments/environment';
import { TvShow } from '../types/tv-show';

@Injectable({
  providedIn: 'root'
})
export class TvShowService {

  constructor(private httpClient: HttpClient) {
  }

  list(): Observable<Array<TvShow>> {
    return this.httpClient.get<Array<TvShow>>(`${environment.keepTrackApiUrl}/api/tv-shows`);
  }

  create(input: TvShow): Observable<TvShow> {
    return this.httpClient.post<TvShow>(`${environment.keepTrackApiUrl}/api/tv-shows`, input);
  }

  delete(input: TvShow): Observable<number> {
    return this.httpClient.delete<number>(`${environment.keepTrackApiUrl}/api/tv-shows/${input.id}`);
  }

}
