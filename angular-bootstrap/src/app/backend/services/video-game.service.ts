import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { VideoGame } from '../types/video-game';

@Injectable({
  providedIn: 'root'
})
export class VideoGameService {

  constructor(private httpClient: HttpClient) {
  }

  list(): Observable<Array<VideoGame>> {
    return this.httpClient.get<Array<VideoGame>>(`${environment.keepTrackApiUrl}/api/video-games`);
  }

  create(input: VideoGame): Observable<VideoGame> {
    return this.httpClient.post<VideoGame>(`${environment.keepTrackApiUrl}/api/video-games`, input);
  }

  delete(input: VideoGame): Observable<number> {
    return this.httpClient.delete<number>(`${environment.keepTrackApiUrl}/api/video-games/${input.id}`);
  }

}
