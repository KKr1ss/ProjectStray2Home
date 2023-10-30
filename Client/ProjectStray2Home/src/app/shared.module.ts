import { NgModule } from "@angular/core";
import { DaysSinceStatusChangePipe } from "./shared/pipes/days-since-status-change.pipe";
import { AnimalStatusConvererPipe } from "./shared/pipes/animal-status-converter.pipe";
import { AnimalTypeConvererPipe } from "./shared/pipes/animal-type-converter.pipe";
import { CityFilterPipe } from "./shared/pipes/city-filter.pipe";
import { UserSexConvererPipe } from "./shared/pipes/user-sex-converter.pipe";
import { AnimalSexConvererPipe } from "./shared/pipes/animal-sex-converter.pipe";
import { CommonModule } from "@angular/common";
import { SmallAnimalThumbnailComponent } from "./animal/thumbnails/small-animal-thumbnail/small-animal-thumbnail.component";
import { AngularMaterialModule } from "./angular-material.module";
import { RouterLink } from "@angular/router";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { BlobToSafeURLConverterPipe } from "./shared/pipes/blob-to-safeurl-converter.pipe";

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    AngularMaterialModule,
    RouterLink,
    AngularMaterialModule,
  ],
  declarations: [
    DaysSinceStatusChangePipe,
    AnimalStatusConvererPipe,
    AnimalTypeConvererPipe,
    AnimalSexConvererPipe,
    CityFilterPipe,
    UserSexConvererPipe,
    BlobToSafeURLConverterPipe,
    SmallAnimalThumbnailComponent
  ],
  exports: [
    //Modules
    AngularMaterialModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,

    //Pipes
    DaysSinceStatusChangePipe,
    AnimalStatusConvererPipe,
    AnimalTypeConvererPipe,
    AnimalSexConvererPipe,
    CityFilterPipe,
    UserSexConvererPipe,
    BlobToSafeURLConverterPipe,

    //Component
    SmallAnimalThumbnailComponent,
    
  ]
})
export class SharedModule { }
