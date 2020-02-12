import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { MovieService } from './movie.service';
import { Movie } from '../types/movie';
import { environment } from 'src/environments/environment.dev';

describe('MovieService', () => {

  let movieService: MovieService;
  let http: HttpTestingController;

  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      HttpClientTestingModule
    ]
  }));

  beforeEach(() => {
    http = TestBed.get(HttpTestingController);
    movieService = TestBed.get(MovieService);
  });

  afterAll(() => http.verify());

  it('should list', () => {
    // fake response
    const hardcodedMovies = [{ title: 'Paris' }, { title: 'Tokyo' }, { title: 'Lyon' }] as Array<Movie>;

    let actualMovies: Array<Movie> = [];
    movieService.list().subscribe((movies: Array<Movie>) => actualMovies = movies);

    http.expectOne(`${environment.keepTrackApiUrl}/api/races?status=PENDING`)
      .flush(hardcodedMovies);

    expect(actualMovies).toEqual(hardcodedMovies, 'The `list` method should return an array of Movie wrapped in an Observable');
  });
});
