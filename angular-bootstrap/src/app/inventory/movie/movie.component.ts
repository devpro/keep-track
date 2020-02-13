import { Component, OnInit, OnDestroy } from '@angular/core';
import { MovieService } from 'src/app/backend/services/movie.service';
import { Movie } from 'src/app/backend/types/movie';
import { Subscription } from 'rxjs';
import { AuthenticateService } from 'src/app/user/services/authenticate.service';

@Component({
  selector: 'app-movie',
  templateUrl: './movie.component.html',
  styleUrls: ['./movie.component.css']
})
export class MovieComponent implements OnInit, OnDestroy {

  movies: Array<Movie> = [];
  userEventsSubscription: Subscription;

  constructor(private movieService: MovieService, private authenticateService: AuthenticateService) { }

  ngOnInit() {
    this.userEventsSubscription = this.authenticateService.user.subscribe(user => {
      this.movieService.list()
        .subscribe(
          movies => {
            this.movies = movies;
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
    this.movieService.create({ title }).subscribe(movie => this.movies.push(movie));
  }

  delete(movie: Movie) {
    this.movieService.delete(movie).subscribe(deletedCount => this.movies.splice(this.movies.findIndex(x => x.id === movie.id), 1));
  }

}
