import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { LoginComponent } from './user/login/login.component';
import { MovieComponent } from './inventory/movie/movie.component';
import { BookComponent } from './inventory/book/book.component';
import { CarComponent } from './inventory/car/car.component';
import { AuthGuard } from './auth.guard';
import { TvShowComponent } from './inventory/tv-show/tv-show.component';
import { VideoGameComponent } from './inventory/video-game/video-game.component';

const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'counter', component: CounterComponent },
  { path: 'fetch-data', component: FetchDataComponent },
  { path: 'login', component: LoginComponent },
  { path: 'movies', component: MovieComponent, canActivate: [AuthGuard] },
  { path: 'books', component: BookComponent, canActivate: [AuthGuard] },
  { path: 'cars', component: CarComponent, canActivate: [AuthGuard] },
  { path: 'tv-shows', component: TvShowComponent, canActivate: [AuthGuard] },
  { path: 'video-games', component: VideoGameComponent, canActivate: [AuthGuard] }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
