import { Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Subscription } from 'rxjs';
import { VideoGameService } from 'src/app/backend/services/video-game.service';
import { VideoGame } from 'src/app/backend/types/video-game';
import { AuthenticateService } from 'src/app/user/services/authenticate.service';

@Component({
  selector: 'app-video-game',
  templateUrl: './video-game.component.html',
  styleUrls: ['./video-game.component.css']
})
export class VideoGameComponent implements OnInit, OnDestroy {

  @ViewChild('titleInput') titleInput: ElementRef;

  videoGames: Array<VideoGame> = [];
  userEventsSubscription: Subscription;

  constructor(private videoGameService: VideoGameService, private authenticateService: AuthenticateService) { }

  ngOnInit() {
    this.userEventsSubscription = this.authenticateService.auth.user.subscribe(user => {
      this.videoGameService.list()
        .subscribe(
          videoGames => {
            this.videoGames = videoGames;
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
    this.videoGameService.create({ title }).subscribe(videoGame => {
      this.videoGames.push(videoGame);
      this.titleInput.nativeElement.value = '';
    });
  }

  startEditing(videoGame: VideoGame) {
    videoGame.isEditable = true;
  }

  cancel(videoGame: VideoGame) {
    videoGame.isEditable = false;
    this.videoGameService.get(videoGame.id).subscribe(item => {
      videoGame.title = item.title;
      videoGame.platform = item.platform;
      videoGame.releasedAt = item.releasedAt;
      videoGame.state = item.state;
      videoGame.finishedAt = item.finishedAt;
    });
  }

  update(videoGame: VideoGame) {
    this.videoGameService.update(videoGame).subscribe(updatedCount => videoGame.isEditable = false);
  }

  delete(videoGame: VideoGame) {
    this.videoGameService.delete(videoGame)
      .subscribe(deletedCount => this.videoGames.splice(this.videoGames.findIndex(x => x.id === videoGame.id), 1));
  }

}
