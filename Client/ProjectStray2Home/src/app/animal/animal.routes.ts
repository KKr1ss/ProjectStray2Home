import { Roles } from "../shared/enums/enums";
import { AuthGuard } from "../user/common/auth.guard";
import { AnimalDetailsComponent } from "./animal-details/animal-details.component";
import { AnimalListComponent } from "./animal-list/animal-list.component";
import { AnimalUploadComponent } from "./animal-upload/animal-upload.component";



export const animalRoutes = [
  {
    path: 'upload', component: AnimalUploadComponent,
    data: { role: Roles.User },    canActivate: [AuthGuard]
  },
  { path: '', component: AnimalListComponent },
  { path: ':id', component: AnimalDetailsComponent }
]
