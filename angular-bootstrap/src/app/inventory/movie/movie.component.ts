import { Component, OnInit, OnDestroy, ViewChild, ElementRef } from '@angular/core';
import { Subscription } from 'rxjs';

import { MovieService } from 'src/app/backend/services/movie.service';
import { Movie } from 'src/app/backend/types/movie';
import { AuthenticateService } from 'src/app/user/services/authenticate.service';

@Component({
  selector: 'app-movie',
  templateUrl: './movie.component.html',
  styleUrls: ['./movie.component.css']
})
export class MovieComponent implements OnInit, OnDestroy {

  @ViewChild('titleInput') titleInput= {} as ElementRef;
  @ViewChild('yearInput') yearInput= {} as ElementRef;

  movies: Array<Movie> = [];
  userEventsSubscription: Subscription | undefined;

  constructor(private movieService: MovieService, private authenticateService: AuthenticateService) { }

  ngOnInit() {
    this.userEventsSubscription = this.authenticateService.auth.user.subscribe(() => {
      this.movieService.list().subscribe({
        next: (movies) => this.movies = movies,
        error: (error) => console.warn(error)
      });
    });
  }

  ngOnDestroy() {
    if (this.userEventsSubscription) {
      this.userEventsSubscription.unsubscribe();
    }
  }

  filter(search: string) {
    this.movieService.list(search).subscribe({
      next: (movies) => this.movies = movies,
      error: (error) => console.warn(error)
    });
  }

  create(title: string, year?: number) {
    this.movieService.create({ title, year }).subscribe(movie => {
      this.movies.push(movie);
      this.titleInput.nativeElement.value = '';
    });
  }

  startEditing(movie: Movie) {
    movie.isEditable = true;
  }

  cancel(movie: Movie) {
    movie.isEditable = false;
    if (!movie.id) {
      return;
    }

    this.movieService.get(movie.id).subscribe(item => {
      movie.title = item.title;
      movie.year = item.year;
    });
  }

  update(movie: Movie) {
    this.movieService.update(movie)
      .subscribe(() => movie.isEditable = false);
  }

  delete(movie: Movie) {
    this.movieService.delete(movie).subscribe(() => this.movies.splice(this.movies.findIndex(x => x.id === movie.id), 1));
  }

}
