#ifndef Timer_h
#define Timer_h

class Timer
{
private:
  
  unsigned long timeAnchor;
  float lastSecondTime;
  int lastSecondTicks;

  
public:
  
  float speed, time, totalTime, actualTime, totalActualTime;
  int ticksLastSecond;
  unsigned long totalTicks;

  void Init();
  void Tick();  
};

#endif
// Timer_h
