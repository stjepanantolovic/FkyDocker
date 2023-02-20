import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EnvelopeGridComponent } from './envelope-grid.component';

describe('EnvelopeGridComponent', () => {
  let component: EnvelopeGridComponent;
  let fixture: ComponentFixture<EnvelopeGridComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EnvelopeGridComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EnvelopeGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
