import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MovieComponent } from './movie/movie.component';
import { BookComponent } from './book/book.component';
import { CarComponent } from './car/car.component';
import { VideoGameComponent } from './video-game/video-game.component';
import { TvShowComponent } from './tv-show/tv-show.component';

@NgModule({
  declarations: [
    MovieComponent,
    BookComponent,
    CarComponent,
    VideoGameComponent,
    TvShowComponent
  ],
  imports: [
    CommonModule
  ]
})
export class InventoryModule { }
