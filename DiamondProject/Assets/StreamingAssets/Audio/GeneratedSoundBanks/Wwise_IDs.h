/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID AMB_ARENA = 3854123299U;
        static const AkUniqueID AMB_VISUALNOVEL = 399517452U;
        static const AkUniqueID ATTACK = 180661997U;
        static const AkUniqueID BOSS_FALL_FALLDASH_DASH = 2685031445U;
        static const AkUniqueID BOSS_FALL_FALLDASH_LEAF = 211307037U;
        static const AkUniqueID BOSS_FALL_LEAFBOOMERANG_GROW = 3749318259U;
        static const AkUniqueID BOSS_FALL_LEAFBOOMERANG_MOVE = 909995601U;
        static const AkUniqueID BOSS_FALL_LEAFRAIN = 2422880163U;
        static const AkUniqueID BOSS_FALL_TREE_ATTACK = 1785879112U;
        static const AkUniqueID BOSS_FALL_TREE_SUMMON = 3962528453U;
        static const AkUniqueID BOSS_FORMCHANGE_FALL = 4139264525U;
        static const AkUniqueID BOSS_FORMCHANGE_NEUTRAL = 3651181119U;
        static const AkUniqueID BOSS_FORMCHANGE_WINTER = 4024294733U;
        static const AkUniqueID BOSS_HIT = 1142827732U;
        static const AkUniqueID BOSS_NEUTRAL_BULLETHELL = 1890679200U;
        static const AkUniqueID BOSS_NEUTRAL_EXPLOBUSH_EXPLOSION = 1792841279U;
        static const AkUniqueID BOSS_NEUTRAL_EXPLOBUSH_SUMMON = 738273803U;
        static const AkUniqueID BOSS_NEUTRAL_LEAFRAIN = 655685355U;
        static const AkUniqueID BOSS_NEUTRAL_TREE_SUMMON = 2933364317U;
        static const AkUniqueID BOSS_WINTER_BULLETHELL = 2891304092U;
        static const AkUniqueID BOSS_WINTER_ICEHELL = 3321318127U;
        static const AkUniqueID BOSS_WINTER_SNOWABSORPTION = 2356299211U;
        static const AkUniqueID GAMESTART = 4058101365U;
        static const AkUniqueID PUMPKID_CROSSBOW_SHOT = 2202228643U;
        static const AkUniqueID PUMPKID_DEATH = 2921680214U;
        static const AkUniqueID PUMPKID_FOOTSTEP = 504658162U;
        static const AkUniqueID PUMPKID_HIT = 906040055U;
        static const AkUniqueID PUMPKID_SWORD_ESTOC = 2139406500U;
        static const AkUniqueID PUMPKID_SWORD_SLASH = 971183997U;
        static const AkUniqueID UI_DIALOG_LETTER = 2587768045U;
        static const AkUniqueID UI_DIALOG_PASS = 605617410U;
        static const AkUniqueID UI_MENU_BUTTON_BACK = 4197357760U;
        static const AkUniqueID UI_MENU_BUTTON_CONFIRM = 342937835U;
        static const AkUniqueID UI_MENU_CLOSE = 3226991506U;
        static const AkUniqueID UI_MENU_OPEN = 4083126854U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace ST_GAMEMODE
        {
            static const AkUniqueID GROUP = 1923804120U;

            namespace STATE
            {
                static const AkUniqueID INGAME = 984691642U;
                static const AkUniqueID MENU = 2607556080U;
                static const AkUniqueID NONE = 748895195U;
            } // namespace STATE
        } // namespace ST_GAMEMODE

    } // namespace STATES

    namespace SWITCHES
    {
        namespace SW_BOSSFORM
        {
            static const AkUniqueID GROUP = 440062387U;

            namespace SWITCH
            {
                static const AkUniqueID FALL = 2512384458U;
                static const AkUniqueID NEUTRAL = 670611050U;
                static const AkUniqueID WINTER = 2965343494U;
            } // namespace SWITCH
        } // namespace SW_BOSSFORM

        namespace SW_GROUND
        {
            static const AkUniqueID GROUP = 87535695U;

            namespace SWITCH
            {
                static const AkUniqueID DIRT = 2195636714U;
                static const AkUniqueID ICE = 344481046U;
                static const AkUniqueID LEAF = 54442137U;
            } // namespace SWITCH
        } // namespace SW_GROUND

        namespace SW_PUMPKID_DAMAGETYPE
        {
            static const AkUniqueID GROUP = 1978060648U;

            namespace SWITCH
            {
                static const AkUniqueID EXPLOBUSH = 1098478123U;
                static const AkUniqueID ICE = 344481046U;
                static const AkUniqueID LEAF = 54442137U;
                static const AkUniqueID ROCK = 2144363834U;
                static const AkUniqueID TREE = 3322072369U;
            } // namespace SWITCH
        } // namespace SW_PUMPKID_DAMAGETYPE

        namespace SW_WEAPONTYPE
        {
            static const AkUniqueID GROUP = 3085206758U;

            namespace SWITCH
            {
                static const AkUniqueID CROSSBOW = 2523406417U;
                static const AkUniqueID SWORD = 2454616260U;
            } // namespace SWITCH
        } // namespace SW_WEAPONTYPE

    } // namespace SWITCHES

    namespace GAME_PARAMETERS
    {
        static const AkUniqueID RTPC_BOSS_ATTACKTYPE = 3290513731U;
        static const AkUniqueID RTPC_BOSSFORM = 3679360398U;
        static const AkUniqueID RTPC_BOSSFORM_SLOW = 1139492498U;
        static const AkUniqueID RTPC_PUMPKID_SPEED = 3854188899U;
        static const AkUniqueID RTPC_WEAPONTYPE = 90049299U;
    } // namespace GAME_PARAMETERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID MAIN = 3161908922U;
        static const AkUniqueID MASTER = 4056684167U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID AMB = 1117531639U;
        static const AkUniqueID BOSS = 1560169506U;
        static const AkUniqueID CROSSBOW = 2523406417U;
        static const AkUniqueID FALL = 2512384458U;
        static const AkUniqueID FOLEY = 247557814U;
        static const AkUniqueID HIT = 1116398592U;
        static const AkUniqueID MASTER = 4056684167U;
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
        static const AkUniqueID MUSIC = 3991942870U;
        static const AkUniqueID NEUTRAL = 670611050U;
        static const AkUniqueID PUMPKID = 2863832581U;
        static const AkUniqueID SWORD = 2454616260U;
        static const AkUniqueID UI = 1551306167U;
        static const AkUniqueID WINTER = 2965343494U;
    } // namespace BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
