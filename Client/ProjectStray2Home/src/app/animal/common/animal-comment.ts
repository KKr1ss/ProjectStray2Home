import { UserPreview } from "../../user/common/models/user-preview";

export interface AnimalComment {
  id: number;
  comment: string;
  createDate: Date;
  user: UserPreview;
}
