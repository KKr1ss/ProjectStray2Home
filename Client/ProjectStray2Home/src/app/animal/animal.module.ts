import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { animalRoutes } from "./animal.routes";
import { AnimalListComponent } from "./animal-list/animal-list.component";
import { AnimalUploadComponent } from "./animal-upload/animal-upload.component";
import { AnimalService } from "./common/animal.service";
import { AnimalDetailsComponent } from './animal-details/animal-details.component';
import { SharedModule } from "../shared.module";
import { AnimalThumbnailComponent } from "./thumbnails/animal-thumbnail/animal-thumbnail.component";

@NgModule({
  imports: [
    RouterModule.forChild(animalRoutes),
    SharedModule
  ],
  declarations: [
    AnimalListComponent,
    AnimalUploadComponent,
    AnimalThumbnailComponent,
    AnimalDetailsComponent
  ],
  providers: [
    AnimalService
  ]
})
export class AnimalModule { }
