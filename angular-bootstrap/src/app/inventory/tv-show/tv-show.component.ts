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

  @ViewChild('titleInput') titleInput: ElementRef;

  tvShows: Array<TvShow> = [];
  userEventsSubscription: Subscription;

  constructor(private tvShowService: TvShowService, private authenticateService: AuthenticateService) { }

  ngOnInit() {
    this.userEventsSubscription = this.authenticateService.user.subscribe(user => {
      this.tvShowService.list()
        .subscribe(
          books => {
            this.tvShows = books;
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
    this.tvShowService.create({ title }).subscribe(tvShow => {
      this.tvShows.push(tvShow);
      this.titleInput.nativeElement.value = '';
    });
  }

  delete(tvShow: TvShow) {
    this.tvShowService.delete(tvShow).subscribe(deletedCount => this.tvShows.splice(this.tvShows.findIndex(x => x.id === tvShow.id), 1));
  }

}
