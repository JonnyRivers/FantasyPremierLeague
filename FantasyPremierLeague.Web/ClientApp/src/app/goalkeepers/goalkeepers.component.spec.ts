import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GoalkeepersComponent } from './goalkeepers.component';

describe('GoalkeepersComponent', () => {
  let component: GoalkeepersComponent;
  let fixture: ComponentFixture<GoalkeepersComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GoalkeepersComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GoalkeepersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
