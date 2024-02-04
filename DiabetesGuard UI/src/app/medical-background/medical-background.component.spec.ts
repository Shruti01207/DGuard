import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MedicalBackgroundComponent } from './medical-background.component';

describe('MedicalBackgroundComponent', () => {
  let component: MedicalBackgroundComponent;
  let fixture: ComponentFixture<MedicalBackgroundComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MedicalBackgroundComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(MedicalBackgroundComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
