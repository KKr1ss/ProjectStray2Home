import { UserPreview } from "../../user/common/models/user-preview";
import { AnimalComment } from "./animal-comment";

export interface AnimalDetails {
  id: number;
  type: string;
  breed: string;
  name: string;
  sex: string;
  dateOfBirth?: Date;
  characteristic: string;
  behavior: string;
  isChipped: boolean;
  status: string;
  statusDescription?: string;
  city: string;
  animalImagesId: number[]
  userPreview: UserPreview;
  comments: AnimalComment[];
}
