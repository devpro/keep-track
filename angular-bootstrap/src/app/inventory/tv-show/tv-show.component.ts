import { Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Subscription } from 'rxjs';
import { TvShowService } from 'src/app/backend/services/tv-show.service';
import { TvShow } from 'src/app/backend/types/tv-show';
import { AuthenticateService } from 'src/app/user/services/authenticate.service';

@Component({
  selector: 'app-tv-show',
  templateUrl: './tv-show.component.html',
  styleUrls: ['./tv-show.component.css']
})
export class TvShowComponent implements OnInit, OnDestroy {

  @ViewChild('titleInput') titleInput= {} as ElementRef;

  tvShows: Array<TvShow> = [];
  userEventsSubscription: Subscription | undefined;

  constructor(private tvShowService: TvShowService, private authenticateService: AuthenticateService) { }

  ngOnInit() {
    this.userEventsSubscription = this.authenticateService.authState$.subscribe(() => {
      this.tvShowService.list().subscribe({
        next: tvShows => this.tvShows = tvShows,
        error: error => console.warn(error)
      });
    });
  }

  ngOnDestroy() {
    if (this.userEventsSubscription) {
      this.userEventsSubscription.unsubscribe();
    }
  }

  filter(search: string) {
    this.tvShowService.list(search).subscribe({
      next: (tvShows) => this.tvShows = tvShows,
      error: (error) => console.warn(error)
    });
  }

  create(title: string) {
    this.tvShowService.create({ title }).subscribe(tvShow => {
      this.tvShows.push(tvShow);
      this.titleInput.nativeElement.value = '';
    });
  }

  delete(tvShow: TvShow) {
    this.tvShowService.delete(tvShow).subscribe(deletedCount => this.tvShows.splice(this.tvShows.findIndex(x => x.id === tvShow.id), 1));
  }

}
