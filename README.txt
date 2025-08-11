This is a legacy version of the OpenCNC. It is the last version before OpenCNC starts using OpenIoT for firmware/hardware.

OpenCNC1.0 is the last version before OpenCNC starts using the OpenIoT firmware.
Starting next version, OpenCNC2.0, OpenCNC will no longer have its own separate firmware, but instead use OpenIoT firmware, which is also an open source product and a part of the Palitri infrastructure.
OpenIoT will add to OpenCNC all features and device support that come with it and will simplify OpenCNC by removing the firmware part and leaving it a purely software project.

As a consequence, firmware/hardware in OpenCNC2.0 will be funcionally agnostic - it won't "know", nor "care" what it is and what it does - it's just a bunch of electronics, wired together, cultivated and interfaced by OpenIoT and controlled by OpenCNC
Also, the OpenCNC device will be completely compliant with the OpenIoT specificaiton and will be able to be accessed and controlled via standard OpenIoT apps, just as any other OpenIoT device.
In a sense, the OpenCNC app will just function as a more convenient, use case specific way to use the device