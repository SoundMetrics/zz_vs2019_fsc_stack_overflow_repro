// Copyright 2014-2019 Sound Metrics Corp. All Rights Reserved.

namespace Aris.Model

open Microsoft.FSharp.Data.UnitSystems.SI.UnitSymbols
open System
open System.Runtime.InteropServices

// warning FS0009: Uses of this construct may result in the generation of unverifiable .NET IL code. This warning can be disabled using '--nowarn:9' or '#nowarn "9"'.
#nowarn "9"

module Constants =
    [<Literal>]
    let FrameHeaderSize = 1024

    let Epoch = DateTime(1970, 1, 1)


[<Struct>]
[<StructLayout(LayoutKind.Sequential, Pack=1, CharSet=CharSet.Ansi, Size=Constants.FrameHeaderSize)>]
type internal FrameHeader =
    val mutable frameIndex: int32

    // In Aris FrameTime is timestamp recorded in the sonar: microseconds since epoch (Jan 1st 1970)
    val frameTime: uint64   // V03: 8 unsigned chars, time_t = 4 unsigned chars   V02: 4 unsigned chars, time_t = long int = int for Win95

    val version: uint32     // keep unique version number for frame data
    val status: uint32      // status

        //struct {
        //    uint64 PCTimeStamp;
        //};
    val mutable TS_Year: uint32     // slot shared with Aris uint64 PCTimeStamp  (low)
    val mutable TS_Month: uint32    // slot shared with Aris uint64 PCTimeStamp  (high)
    val TS_Day: uint32
    val TS_Hour: uint32
    val TS_Minute: uint32
    val TS_Second: uint32
    val TS_HSecond: uint32  // 48 unsigned chars

    val transmitMode: uint32  // bit2 = DOUBLE, bit1 = ENABLE, bit0 = HF_MODE
    val windowStart: float32
    val windowLength: float32  // values in frame are status values from sonar
    val threshold: uint32    // 64 unsigned chars
    val intensity: uint32
    val receiverGain: uint32
    val degC1: uint32
    val degC2: uint32
    val humidity: uint32
    val focus: uint32
    val battery: uint32      // 92 unsigned chars to here
    val userValue1: float32    // replaced cStatus1[] 9 March 2006
    val userValue2: float32    // replaced cStatus1[] 9 March 2006
    val userValue3: float32    // replaced cStatus1[] 9 March 2006
    val userValue4: float32    // replaced cStatus1[] 9 March 2006
    val userValue5: float32    // replaced cStatus2[] 9 March 2006
    val userValue6: float32    // replaced cStatus2[] 9 March 2006
    val userValue7: float32    // replaced cStatus2[] 9 March 2006
    val userValue8: float32    // replaced cStatus2[] 9 March 2006 (124 unsigned chars)
    val velocity: float32    // added in V02, 6 November 2001
    val depth: float32<m>      // added in V02, 6 November 2001
    val altitude: float32    // added in V02, 6 November 2001
    val pitch: float32      // added in V02, 6 November 2001
    val pitchRate: float32    // added in V02, 6 November 2001
    val roll: float32      // added in V02, 6 November 2001
    val rollRate: float32    // added in V02, 6 November 2001
    val heading: float32      // added in V02, 6 November 2001
    val headingRate: float32    // added in V02, 6 November 2001 (160 unsigned chars)
    val compassHeading: float32  // 164 unsigned chars to here, renamed 10 March 2008
    val compassPitch: float32  // 168 unsigned chars to here, renamed 10 March 2008
    val compassRoll: float32    // 172 unsigned chars to here, renamed 10 March 2008
    val latitude: float    // 180 unsigned chars to here, added in V02, 6 November 2001 (must align on 8-unsigned char boundary?)
    val longitude: float    // 188 unsigned chars to here, added in V02, 6 November 2001 (must align on 8-unsigned char boundary?)
    val sonarPosition: float32  // 192 unsigned chars to here
    val configFlags: uint32    // 196 unsigned chars to here, added 13 May 2003 for multiple clock sources and future use
    val beamTilt: float32    // 200 unsigned chars to here, added 26 April 2004 for split-body prism function (obsolete)
    val targetRange: float32    // 204 unsigned chars to here, added 14 September 2004 for target tracking output storage
    val targetBearing: float32  // 208 unsigned chars to here, added 14 September 2004 for target tracking output storage
    val targetPresent: uint32  // 212 unsigned chars to here, added 14 September 2004 for target tracking output storage
    val firmwareRevision: uint32// 216 unsigned chars to here, added 1 April 2005 for sonar firmware revision storage
    val flags: uint32      // 220 unsigned chars to here, added 26 July 2005 for topside processing use
    val sourceFrame: uint32    // 224 unsigned chars to here, added 16 November 2006 for topside processing use (record CSOT source frame)
    val waterTemp: float32<degC>    // 228 unsigned chars to here, added 19 February 2007 to store NMEA MTW input
    val timerPeriod: uint32    // 232 unsigned chars to here, added 18 June 2007 for non-integral frame rates and frame rates changing within file
    val sonarX: float32      // 236 unsigned chars to here, added 26 December 2007 for sonar x-axis position for 3D source image files (was UserValue5)
    val sonarY: float32      // 240 unsigned chars to here, added 26 December 2007 for sonar y-axis position for 3D source image files (was UserValue6)
    val sonarZ: float32      // 244 unsigned chars to here, added in V04, 6 March 2008 for sonar z-axis position for 3D source image files (was Depth)
    val sonarPan: float32    // 248 unsigned chars to here, added in V04, 6 March 2008 for mechanical sonar pan rotation
    val sonarTilt: float32    // 252 unsigned chars to here, added in V04, 6 March 2008 for mechanical sonar tilt rotation
    val sonarRoll: float32    // 256 unsigned chars to here, added in V04, 6 March 2008 for mechanical sonar roll rotation (start of extended V04 header)
    val panPNNL: float32      // 260 unsigned chars to here, legacy PNNL aux input data
    val tiltPNNL: float32    // 264 unsigned chars to here
    val rollPNNL: float32    // 268 unsigned chars to here
    val vehicleTime: float    // 276 unsigned chars to here, (must be on 8-unsigned char boundary)...seconds since 1 Jan 1970 with fractional part for exact frame time
    val timeGGK: float32      // 280 unsigned chars to here, GGK input data - ascii string converted to float
    val dateGGK: uint32 // 284 unsigned chars to here, GGK input data - ascii mmddyy string converted to uint32
    val qualityGGK: uint32    // 288 unsigned chars to here, GGK input data
    val numSatsGGK: uint32    // 292 unsigned chars to here, GGK input data
    val dOPGGK: float32;      // 296 unsigned chars to here, GGK input data
    val eHTGGK: float32;      // 300 unsigned chars to here, GGK input data
    val heaveTSS: float32    // 304 unsigned chars to here, TSS input data
    val yearGPS: uint32 // 308 unsigned chars to here, GPS input data - GPS time data
    val monthGPS: uint32    // 312 unsigned chars to here, GPS input data - GPS time data
    val dayGPS: uint32 // 316 unsigned chars to here, GPS input data - GPS time data
    val hourGPS: uint32 // 320 unsigned chars to here, GPS input data - GPS time data
    val minuteGPS: uint32    // 324 unsigned chars to here, GPS input data - GPS time data
    val secondGPS: uint32    // 328 unsigned chars to here, GPS input data - GPS time data
    val hSecondGPS: uint32    // 332 unsigned chars to here, GPS input data - GPS time data
    val sonarPanOffset: float32  // 336 unsigned chars to here, sonar mount "pan 0" rotation about Z axis (e.g. look left == -90)
    val sonarTiltOffset: float32 // 340 unsigned chars to here, sonar mount "tilt 0" rotation about local sonar Y axis  (e.g. look up == +90)
    val sonarRollOffset: float32 // 344 unsigned chars to here, sonar mount "roll 0" rotation about local sonar X axis  (e.g. scan vertical surface == +/-90)
    val sonarXOffset: float32    // 348 unsigned chars to here, sonar mount x offset to platform center of rotation along X axis
    val sonarYOffset: float32    // 352 unsigned chars to here, sonar mount y offset to platform center of rotation along Y axis
    val sonarZOffset: float32    // 356 unsigned chars to here, sonar mount z offset to platform center of rotation along Z axis
    val tMatrix00: float32     // 420 unsigned chars to here, net rotation/translation matrix from local sonar XYZ to vehicle/platform XYZ to "world space" XYZ
    val tMatrix01: float32
    val tMatrix02: float32
    val tMatrix03: float32
    val tMatrix04: float32
    val tMatrix05: float32
    val tMatrix06: float32
    val tMatrix07: float32
    val tMatrix08: float32
    val tMatrix09: float32
    val tMatrix10: float32
    val tMatrix11: float32
    val tMatrix12: float32
    val tMatrix13: float32
    val tMatrix14: float32
    val tMatrix15: float32
    val sampleRate: float32     // still needed?
    val accellX: float32
    val accellY: float32
    val accellZ: float32

    val pingMode: uint32
    val frequencyHiLow: uint32
    val pulseWidth: int<Us>
    val cyclePeriod: int<Us>
    val samplePeriod: int<Us>
    val transmitEnable: uint32
    val frameRate: float32</s>      // commanded Frame Rate
    val soundSpeed: float32<m/s>
    val samplesPerBeam: int
    val enable150V: uint32
    val sampleStartDelay: int<Us>
    val largeLens: uint32
    val theSystemType: uint32

    val sonarSerialNumber: uint32
    val encryptedKey: uint64

    val arisErrorFlagsUint: uint32 // see FileTraitsConsts.h

    val missedPackets: uint32
    val arisAppVersion: uint32     // This is actually ArisAppVersionSubMinor (badly named)
    val frameDelta: uint32         // Microseconds delay since last frame: uint32 if GoTime is non-zero this is based on GoTime rather than
                                    // the sonar timestamp.
    val mutable reorderedSamples: uint32
    val salinity: uint32
    val pressure: float32

    val batteryVoltage: float32           // added 10-May-2012 for SPI devices
    val mainVoltage: float32
    val switchVoltage: float32

    val focusMotorMoving: uint32   // added 14-Aug-2012 for AutomaticRecording
    val voltageChanging: uint32    // added 16-Aug (first two bits = 12V, second two bits = 150V, 00 = not changing, 01 = turning on, 10 = turning off)

    val focusTimeoutFault: uint32
    val focusOverCurrentFault: uint32
    val focusNotFoundFault: uint32
    val focusStalledFault: uint32

    val fPGATimeoutFault: uint32
    val fPGABusyFault: uint32
    val fPGAStuckFault: uint32

    val cPUTempFault: uint32
    val pSUTempFault: uint32
    val waterTempFault: uint32
    val humidityFault: uint32
    val pressureFault: uint32
    val voltageReadFault: uint32
    val voltageWriteFault: uint32

    val focusCurrentPosition: uint32

    val targetPan: float32
    val targetTilt: float32
    val targetRoll: float32

    val panMotorErrorCode: uint32
    val tiltMotorErrorCode: uint32
    val rollMotorErrorCode: uint32

    val panAbsPosition: float32
    val tiltAbsPosition: float32
    val rollAbsPosition: float32

    val panAccelX: float32
    val panAccelY: float32
    val panAccelZ: float32
    val tiltAccelX: float32
    val tiltAccelY: float32
    val tiltAccelZ: float32
    val rollAccelX: float32
    val rollAccelY: float32
    val rollAccelZ: float32

    val appliedSettings: uint32
    val constrainedSettings: uint32
    val invalidSettings: uint32

    val enableInterpacketDelay: uint32
    val interpacketDelayPeriod: uint32

    val uptime: uint32 // total number of seconds sonar has been running
    val arisAppVersionMajor: uint16
    val arisAppVersionMinor: uint16
    val goTime: uint64

    val panVelocity: float32
    val tiltVelocity: float32
    val rollVelocity: float32
with
    static member From buf (timestamp: DateTimeOffset) =
        let h = GCHandle.Alloc(buf, GCHandleType.Pinned)
        try
            let mutable hdr = Marshal.PtrToStructure(h.AddrOfPinnedObject(), typeof<FrameHeader>) :?> FrameHeader

            //-----------------------------------------------------------------
            // Set the PC timestamp. Needs to be done with DateTime rather than
            // DateTimeOffset so that 3pm DST shows up as 3pm on the topside.

            // A tick is 100ns. We need microseconds here.
            let ticks = (timestamp.DateTime - Constants.Epoch).Ticks
            let us = ticks / 10L
            let hi = uint32 (us >>> 32)
            let lo = uint32 (us &&& 0x00000000FFFFFFFFL)

            hdr.TS_Month <- hi
            hdr.TS_Year <- lo

            hdr
        finally
            h.Free()
