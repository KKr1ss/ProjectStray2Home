import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { of } from 'rxjs';
import { ImageDownloadService } from '../../../common/image-download.service';
import { UserPreview } from '../../../user/common/models/user-preview';
import { AnimalPreview } from '../../common/animal-preview';

import { AnimalThumbnailComponent } from './animal-thumbnail.component';

describe('AnimalThumbnailComponent', () => {
  let component: AnimalThumbnailComponent;
  let fixture: ComponentFixture<AnimalThumbnailComponent>;

  //declare test service
  let imageDownloadService: jasmine.SpyObj<ImageDownloadService>

  //declare test datas
  const userPreview: UserPreview = {
      id: 'id',
      firstName: 'TestF',
      lastName: 'TestL',
      sex: 'Male',
      currentCity: 'TestCity2',
      userName: 'Tester',
      email: 'test@test.com',
      phoneNumber: '3630'
  }
  const animalPreview: AnimalPreview = {
      id: 1,
      type: 'TestType',
      breed: 'TestBreed',
      name: 'TestName',
      sex: 'Female',
      status: 'Lost',
      city: 'TestCity',
      userPreview: userPreview,
      statusDate: new Date()
  }

  beforeEach(async () => {
    //implement fake imageDownloadService
    imageDownloadService = jasmine.createSpyObj<ImageDownloadService>('ImageDownloadService', ['getUserProfileImage', 'getAnimalImage'])
    imageDownloadService.getUserProfileImage.and.returnValue(of(new Blob))
    imageDownloadService.getAnimalImage.and.returnValue(of(new Blob))

    await TestBed.configureTestingModule({
      declarations: [AnimalThumbnailComponent],
      imports: [
        RouterModule
      ],
      providers: [
        { provide: ImageDownloadService, useValue: imageDownloadService },
        {
          provide: ActivatedRoute,
          useValue: {}
        }
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AnimalThumbnailComponent);
    component = fixture.componentInstance;
    component.animal = animalPreview;
  });

  it('should create component', () => {
    fixture.detectChanges();
    expect(component).toBeTruthy();
  });

  it('should get images', async () => {
    fixture.detectChanges();
    await fixture.whenStable()
    fixture.detectChanges();

    const animalImage = fixture.debugElement.nativeElement.querySelector("#animalImage");
    const userImage = fixture.debugElement.nativeElement.querySelector("#userImage");
    expect(animalImage['src']).toContain('.png');
    expect(userImage['src']).toContain('.jpg');
  });
});
