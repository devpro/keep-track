import { NgModule } from '@angular/core';
import { AngularFireAuthGuard } from '@angular/fire/compat/auth-guard';
import { Routes, RouterModule } from '@angular/router';

import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { HomeComponent } from './home/home.component';
import { BookComponent } from './inventory/book/book.component';
import { CarComponent } from './inventory/car/car.component';
import { MovieComponent } from './inventory/movie/movie.component';
import { TvShowComponent } from './inventory/tv-show/tv-show.component';
import { VideoGameComponent } from './inventory/video-game/video-game.component';
import { LoginComponent } from './user/login/login.component';

const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'counter', component: CounterComponent },
  { path: 'fetch-data', component: FetchDataComponent },
  { path: 'login', component: LoginComponent },
  { path: 'movies', component: MovieComponent, canActivate: [AngularFireAuthGuard] },
  { path: 'books', component: BookComponent, canActivate: [AngularFireAuthGuard] },
  { path: 'cars', component: CarComponent, canActivate: [AngularFireAuthGuard] },
  { path: 'tv-shows', component: TvShowComponent, canActivate: [AngularFireAuthGuard] },
  { path: 'video-games', component: VideoGameComponent, canActivate: [AngularFireAuthGuard] }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, {})
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
