import { ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { ActivatedRoute, convertToParamMap, RouterModule } from '@angular/router';
import { of } from 'rxjs';
import { ImageDownloadService } from '../../common/image-download.service';
import { UserPreview } from '../../user/common/models/user-preview';
import { AnimalDetails } from '../common/animal-details';
import { AnimalService } from '../common/animal.service';
import { AnimalDetailsComponent } from './animal-details.component';

describe('AnimalDetailsComponent', () => {
  let component: AnimalDetailsComponent;
  let fixture: ComponentFixture<AnimalDetailsComponent>;

  let animalService: jasmine.SpyObj<AnimalService>;
  let imageDownloadService: jasmine.SpyObj<ImageDownloadService>

  const userPreview: UserPreview = {
      id: 'id',
      firstName: 'TestF',
      lastName: 'TestL',
      sex: 'Male',
      currentCity: 'TestCity2',
      userName: 'Tester',
      email: 'test@test.com',
      phoneNumber: '36307794073'
  }
  const animalDetails: AnimalDetails = {
      id: 1,
      type: 'TestType',
      breed: 'TestBreed',
      name: 'TestName',
      sex: 'Male',
      characteristic: 'TestChar',
      behavior: 'TestBeh',
      isChipped: false,
      status: 'Lost',
      city: 'TestCity',
      animalImagesId: [11, 22],
      userPreview: userPreview,
      comments: []
  }

  beforeEach(async () => {
    //implement fake animal service

    animalService = jasmine.createSpyObj<AnimalService>('AnimalService', ['getAnimalDetails'])
    animalService.getAnimalDetails.and.returnValue(of(animalDetails))

    //implement fake imageDownloadService
    imageDownloadService = jasmine.createSpyObj<ImageDownloadService>('ImageDownloadService', ['getUserProfileImage', 'getAnimalImageByImageId'])
    imageDownloadService.getUserProfileImage.and.returnValue(of(new Blob))
    imageDownloadService.getAnimalImageByImageId.and.returnValue(of(new Blob))
    //imageDownloadService.convertBlobToSafeUrl.and.returnValue(of())

    await TestBed.configureTestingModule({
      declarations: [AnimalDetailsComponent],
      imports: [
        RouterModule
      ],
      providers: [
        { provide: AnimalService, useValue: animalService },
        { provide: ImageDownloadService, useValue: imageDownloadService },
        {
          provide: ActivatedRoute,
          useValue: {
            snapshot: {
              paramMap: convertToParamMap({
                id: '1'
              })
            }
          }
        }
      ]
    })
      .compileComponents();

    fixture = TestBed.createComponent(AnimalDetailsComponent);
    component = fixture.componentInstance;
  });

  it('should create component', () => {
    fixture.detectChanges();
    expect(component).toBeTruthy();
  });

  it('should get animal details', async () => {
    fixture.detectChanges();
    await fixture.whenStable()
    fixture.detectChanges();

    const h1AnimalName = fixture.debugElement.query(By.css('h1'));
    const animalImage = fixture.debugElement.nativeElement.querySelector("#animalImage2");
    expect(animalService.getAnimalDetails).toHaveBeenCalled();
    expect(h1AnimalName.nativeElement.textContent).toEqual(animalDetails.name)
    expect(animalImage['src']).toContain('.png');
  })

  it('should get animal images', async () => {
    fixture.detectChanges();
    await fixture.whenStable()
    fixture.detectChanges();

    const animalImage1 = fixture.debugElement.nativeElement.querySelector("#animalImage1");
    const animalImage2 = fixture.debugElement.nativeElement.querySelector("#animalImage2");
    expect(animalImage1['src']).toContain('.png');
    expect(animalImage2['src']).toContain('.png');
    expect(component.animalImages[0].id).toEqual(1)
    expect(component.animalImages[1].id).toEqual(2)
  })
});
